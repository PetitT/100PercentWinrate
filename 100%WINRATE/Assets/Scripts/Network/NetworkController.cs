using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] private int maxPlayers;

    [SerializeField] private Button startButton;
    [SerializeField] private GameObject connectingImage;

    private void Start()
    {
        Debug.Log("Tried to Connect");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player Connected");
        PhotonNetwork.AutomaticallySyncScene = true;
        connectingImage.SetActive(false);
        startButton.gameObject.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Failed to connect");
        connectingImage.GetComponentInChildren<Text>().text = "Failed to connect...";
    }

    public void JoinRoom()
    {
        Debug.Log("Attempt to Join Room");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No existing room");
        CreateRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("Creating room");
        int random = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxPlayers, CleanupCacheOnLeave = true };
        PhotonNetwork.CreateRoom("Room " + random, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room");
        CreateRoom();
    }

}
