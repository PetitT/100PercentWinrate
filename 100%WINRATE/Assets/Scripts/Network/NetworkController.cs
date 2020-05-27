using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;
using HashTable = ExitGames.Client.Photon.Hashtable;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] private int maxFfaPlayers;
    [SerializeField] private int maxDuelPlayers;

    [SerializeField] private Button startFfaButton;
    [SerializeField] private GameObject connectingFfaImage;

    [SerializeField] private Button startDuelButton;
    [SerializeField] private GameObject connectingDuelImage;

    HashTable ffaTable = new HashTable();
    HashTable duelTable = new HashTable();
    RoomOptions ffaOptions;
    RoomOptions duelOptions;

    RoomOptions selectedOptions;

    private void Start()
    {
        ffaTable.Add("Type", "FreeForAll");
        duelTable.Add("Type", "Duel");

        ffaOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxFfaPlayers, CleanupCacheOnLeave = true };
        ffaOptions.CustomRoomProperties = ffaTable;
        ffaOptions.CustomRoomPropertiesForLobby = new string[] { "Type" };

        duelOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxDuelPlayers, CleanupCacheOnLeave = true };
        duelOptions.CustomRoomProperties = duelTable;
        duelOptions.CustomRoomPropertiesForLobby = new string[] { "Type" };

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        connectingFfaImage.SetActive(false);
        startFfaButton.gameObject.SetActive(true);
        connectingDuelImage.SetActive(false);
        startDuelButton.gameObject.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Failed to connect");
        connectingFfaImage.GetComponentInChildren<Text>().text = "Failed to connect...";
    }

    public void JoinFreeForAll()
    {
        selectedOptions = ffaOptions;
        PhotonNetwork.JoinRandomRoom(null, (byte)maxFfaPlayers, MatchmakingMode.FillRoom, TypedLobby.Default, null);
    }

    public void JoinDuel()
    {
        selectedOptions = duelOptions;
        PhotonNetwork.JoinRandomRoom(null, (byte)maxDuelPlayers, MatchmakingMode.FillRoom, TypedLobby.Default, null);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        int random = Random.Range(0, 10000);
        PhotonNetwork.CreateRoom("Room " + random, selectedOptions, TypedLobby.Default);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
        CreateRoom();
    }

}
