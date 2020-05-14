using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] private string GameSceneName;
    public UnityEvent onJoinedRoom;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        onJoinedRoom?.Invoke();
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Start Game");
            PhotonNetwork.LoadLevel(GameSceneName);
        }
    }
}
