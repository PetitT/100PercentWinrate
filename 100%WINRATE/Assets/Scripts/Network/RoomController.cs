using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] private string GameSceneName;
    [SerializeField] private string DuelSceneName;
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
        onJoinedRoom?.Invoke();
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("FreeForAll"))
            {
                PhotonNetwork.LoadLevel(GameSceneName);
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("Duel"))
            {
                PhotonNetwork.LoadLevel(DuelSceneName);
            }
        }
    }
}
