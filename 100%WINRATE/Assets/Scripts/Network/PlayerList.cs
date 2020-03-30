using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerList : PunSingleton<PlayerList>
{
    [SerializeField] private Transform entryParent;
    private Dictionary<PhotonView, GameObject> playerDisplay = new Dictionary<PhotonView, GameObject>();

    public void AddPlayer(PhotonView player)
    {
        photonView.RPC("AddPlayerEntry", RpcTarget.AllBuffered, PlayerInfo.Instance.Data.playerName, player.ViewID);
    }

    public void RemovePlayer(PhotonView player)
    {
        photonView.RPC("RemovePlayerEntry", RpcTarget.AllBuffered, player.ViewID);
    }

    public void UpdateList(PhotonView player, int score)
    {

    }

    [PunRPC]
    private void AddPlayerEntry(string playerName, int playerID)
    {
        GameObject newEntry = PhotonNetwork.InstantiateSceneObject(Path.Combine(StringsManager.Instance.photon, StringsManager.Instance.playerEntry), Vector3.zero, Quaternion.identity);
        Text entryText = newEntry.GetComponent<Text>();
        entryText.text = playerName + " - " + "0";
        entryText.transform.SetParent(entryParent);
        playerDisplay.Add(PhotonView.Find(playerID), newEntry);
    }

    [PunRPC]
    private void RemovePlayerEntry(int playerID)
    {
       GameObject entryToRemove = playerDisplay[PhotonView.Find(playerID)];
       playerDisplay.Remove(PhotonView.Find(playerID));
       PhotonNetwork.Destroy(entryToRemove);
    }
}
