using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    }

    [PunRPC]
    private void AddPlayerEntry(Player playerToAdd)
    {
        GameObject newEntry = Instantiate(playerEntry);
        newEntry.transform.SetParent(entryParent);
        newEntry.GetComponent<PlayerEntry>().Initialize(playerToAdd.NickName);
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
        playerDisplay[player].GetComponent<PlayerEntry>().Score = score;
        RearrangeList();
    }

    private void RearrangeList()
    {
        var ordered = playerDisplay.OrderByDescending(x => x.Value.GetComponent<PlayerEntry>().Score);
        entryParent.transform.DetachChildren();

        foreach (var entry in ordered)
        {
            entry.Value.transform.SetParent(entryParent.transform);
        }
    }
}
