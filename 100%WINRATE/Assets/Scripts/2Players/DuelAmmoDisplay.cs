using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelAmmoDisplay : MonoBehaviourPun
{
    [SerializeField] private GameObject ammoParent;
    private GameObject ammoUI;
    private List<GameObject> ammos = new List<GameObject>();
    private bool hasInitialized = false;

    private void Start()
    {
        ammoUI = DuelDataManager.Instance.ammoUI;
        photonView.RPC("CreateAmmo", RpcTarget.AllBuffered);
    }

    public void DisplayAmmo(int ammo)
    {
        if (!hasInitialized)
        {
            hasInitialized = true;
        }
        photonView.RPC("Display", RpcTarget.AllBuffered, ammo);
    }

    [PunRPC]
    private void CreateAmmo()
    {
        int maxAmmo = DuelDataManager.Instance.maxAmmo;
        for (int i = 0; i < maxAmmo; i++)
        {
            GameObject newAmmo = Pool.instance.GetItemFromPool(ammoUI, Vector3.zero, Quaternion.identity);
            newAmmo.transform.SetParent(ammoParent.transform);
            ammos.Add(newAmmo);
        }
        for (int i = 0; i < ammos.Count; i++)
        {
            ammos[i].SetActive(false);
        }
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
