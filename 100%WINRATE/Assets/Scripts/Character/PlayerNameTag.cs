using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField] private Text nameText;

    private void Start()
    {
        if (photonView.IsMine)
        {
            string newName = PhotonNetwork.LocalPlayer.NickName;
            photonView.RPC("SetName", RpcTarget.AllBuffered, photonView.ViewID, newName);
            nameText.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    private void SetName(int playerID, string newName)
    {
        nameText.text = newName;
    }
}
