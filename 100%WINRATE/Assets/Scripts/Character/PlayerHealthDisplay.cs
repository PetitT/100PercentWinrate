using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviourPun
{
    [SerializeField] private Text text;

    public void UpdateHealth(float currentHealth)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("DisplayHealth", RpcTarget.AllBuffered, currentHealth);
        }
    }

    [PunRPC]
    private void DisplayHealth(float currentHealth)
    {
        text.text = currentHealth.ToString();
    }
}
