using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuelUIManager : PunSingleton<DuelUIManager>
{
    [SerializeField] private List<TextMeshProUGUI> playerNames;

    public void SetNames()
    {
        photonView.RPC("SetPlayerNames", RpcTarget.All);
    }

    [PunRPC]
    private void SetPlayerNames()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length ; i++)
        {
            playerNames[i].text = players[i].NickName;
        }
    }
}
