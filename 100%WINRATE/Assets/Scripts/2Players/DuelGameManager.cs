using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DuelGameManager : MonoBehaviourPun
{
    [SerializeField] private Transform firstPosition;
    [SerializeField] private Transform secondPosition;

    private void Start()
    {
        string newPlayer = Path.Combine(StringsManager.Instance.duel, StringsManager.Instance.duelPlayer);
        int numberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        Transform playerPosition = numberOfPlayers == 1 ? firstPosition : secondPosition;
        PhotonNetwork.Instantiate(newPlayer, playerPosition.position, Quaternion.identity);
    }
}
