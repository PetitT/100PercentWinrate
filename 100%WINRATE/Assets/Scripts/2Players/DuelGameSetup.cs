using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class DuelGameSetup : MonoBehaviourPun
{
    [SerializeField] private List<Color> playerColors;
    private int lives;
    GameObject localBody;
    int localBodyID;

    private void Start()
    {
        lives = DuelDataManager.Instance.lives;
        CreateAvatar();
        DuelGameManager.Instance.AddPlayer(localBodyID, lives);

        if (PhotonNetwork.IsMasterClient)
        {
            localBody.GetComponent<DuelPlayerManager>().SetID(0);
            photonView.RPC("SetAvatarColor", RpcTarget.AllBuffered, localBodyID, 0);
            DuelUIManager.Instance.AddPlayer(0, localBodyID, PhotonNetwork.LocalPlayer.NickName, lives);
        }

        if (PhotonNetwork.PlayerList.Length == 2)
        {
            localBody.GetComponent<DuelPlayerManager>().SetID(1);
            DuelUIManager.Instance.AddPlayer(1, localBodyID, PhotonNetwork.LocalPlayer.NickName, lives);
            photonView.RPC("SetAvatarColor", RpcTarget.AllBuffered, localBodyID, 1);

            Debug.Log("Start Game");
            StartCoroutine("GameStart");
        }
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1f);
        byte startGameEvent = DuelDataManager.Instance.onGameStartEvent;
        RaiseEventOptions options = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(startGameEvent, null, options, SendOptions.SendReliable);
    }

    private void CreateAvatar()
    {
        string playerPrefab = Path.Combine(StringsManager.Instance.duel, StringsManager.Instance.duelPlayer);
        localBody = PhotonNetwork.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        localBodyID = localBody.GetComponent<PhotonView>().ViewID;
    }

    [PunRPC]
    private void SetAvatarColor(int avatarID, int colorID)
    {
        GameObject avatar = PhotonView.Find(avatarID).gameObject;
        Color playerColor = playerColors[colorID];

        foreach (var renderer in avatar.GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.color = playerColor;
        }
    }
}
