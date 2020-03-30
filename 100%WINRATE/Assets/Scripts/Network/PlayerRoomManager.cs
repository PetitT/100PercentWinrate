using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        if (photonView.IsMine)
        {
            PlayerList.Instance.AddPlayer(photonView);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerList.Instance.RemovePlayer(photonView);
        }
    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            PlayerList.Instance.RemovePlayer(photonView);
        }
    }
}
