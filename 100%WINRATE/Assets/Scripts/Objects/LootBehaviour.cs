using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehaviour : MonoBehaviourPun
{
    public void HideObject()
    {
        photonView.RPC("Hide", RpcTarget.AllBuffered, photonView.ViewID);
    }

    [PunRPC]
    private void Hide(int ID)
    {
        PhotonView.Find(ID).gameObject.SetActive(false);
    }
}
