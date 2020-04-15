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
    public Color playerColor;

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
        float r = playerColor.r;
        float g = playerColor.g;
        float b = playerColor.b;
        float a = playerColor.a;
        photonView.RPC("AddPlayerEntry", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer, r, g, b, a);
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
    private void AddPlayerEntry(Player playerToAdd, float R, float G, float B, float A)
    {
        GameObject newEntry = Instantiate(playerEntry);
        newEntry.transform.SetParent(entryParent);
        Color playerColor = new Color(R, G, B, A);
        newEntry.GetComponent<PlayerEntry>().Initialize(playerToAdd.NickName, playerColor);
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
