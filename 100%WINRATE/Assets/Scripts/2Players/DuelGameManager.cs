using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class DuelGameManager : PunSingleton<DuelGameManager>
{
    [SerializeField] private List<Transform> startPositions;

    private Player[] players;
    private int currentRound;

    private void Start()
    {
        CreateAvatar();
        photonView.RPC("AddNewPlayer", RpcTarget.AllBuffered);
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            StartCoroutine("WaitToStartRound");
        }
    }

    private void CreateAvatar()
    {
        string playerPrefab = Path.Combine(StringsManager.Instance.duel, StringsManager.Instance.duelPlayer);
        GameObject newPlayerObject = PhotonNetwork.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        Hashtable table = new Hashtable();
        int avatarID = newPlayerObject.GetComponentInChildren<AvatarTag>().GetComponent<PhotonView>().ViewID;
        table.Add("ID", avatarID);
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);
    }

    private IEnumerator WaitToStartRound()
    {
        yield return new WaitForSeconds(1);
        photonView.RPC("StartRound", RpcTarget.All);
    }

    [PunRPC]
    private void AddNewPlayer()
    {
        DuelUIManager.Instance.SetNames();
    }

    [PunRPC]
    private void StartRound()
    {
        players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            GameObject avatar = PhotonView.Find((int)players[i].CustomProperties["ID"]).gameObject;
            avatar.transform.position = startPositions[i].position;
        }
    }
}
