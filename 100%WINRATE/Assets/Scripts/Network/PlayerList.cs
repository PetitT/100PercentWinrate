using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerList : MonoBehaviourPunCallbacks
{
    #region singleton
    public static PlayerList instance;

    private void Awake()
    {
        if (instance)
            Destroy(this);
        else
            instance = this;
    }

    #endregion

    [SerializeField] private Transform entryParent;
    [SerializeField] private GameObject playerEntry;
    private Dictionary<Player, GameObject> playerDisplay = new Dictionary<Player, GameObject>();

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        photonView.RPC("AddPlayerEntry", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        photonView.RPC("RemovePlayerEntry", RpcTarget.AllBuffered, otherPlayer);
    }

    
    public void UpdateList(Player player, int score)
    {
        photonView.RPC("UpdatePlayerScores", RpcTarget.AllBuffered, player, score);
        photonView.RPC("RearrangeList", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void AddPlayerEntry(Player playerToAdd)
    {
        GameObject newEntry = Instantiate(playerEntry);
        newEntry.transform.SetParent(entryParent);
        newEntry.GetComponent<Text>().text = playerToAdd.NickName + " - " + "0";
        playerDisplay.Add(playerToAdd, newEntry);
    }

    [PunRPC]
    private void RemovePlayerEntry(Player playerToRemove)
    {
        GameObject itemToRemove = playerDisplay[playerToRemove];
        playerDisplay.Remove(playerToRemove);
        Destroy(itemToRemove);
    }

    [PunRPC]
    private void UpdatePlayerScores(Player player, int score)
    {
        string newText = player.NickName + " - " + score.ToString();
        playerDisplay[player].GetComponent<Text>().text = newText;
    }

    [PunRPC]
    private void RearrangeList()
    {

    }
}
