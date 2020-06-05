using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelAmmoDisplay : MonoBehaviourPun
{
    [SerializeField] private GameObject ammoParent;
    [SerializeField] private List<GameObject> ammos;
    private GameObject ammoUI;
    private bool hasInitialized = false;

    public void DisplayAmmo(int ammo)
    {
        photonView.RPC("Display", RpcTarget.All, ammo);
    }


    [PunRPC]
    private void Display(int ammo)
    {
        for (int i = 0; i < ammos.Count; i++)
        {
            if (i < ammo)
            {
                ammos[i].gameObject.SetActive(true);
            }
            else
            {
                ammos[i].gameObject.SetActive(false);
            }
        }
    }
}
