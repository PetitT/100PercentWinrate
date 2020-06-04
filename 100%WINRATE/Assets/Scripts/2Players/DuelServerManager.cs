using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuelServerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshPro text;

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        StartCoroutine(LeaveGame(otherPlayer.NickName));
    }

    private IEnumerator LeaveGame(string leaver)
    {
        text.text = leaver + " left... \nYou will be kicked out of the room...";
        yield return new WaitForSeconds(5);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("LogScene");
    }
}
