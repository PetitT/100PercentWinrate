using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerSound : MonoBehaviourPun
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private DuelPlayerCollision collision;
    private AudioClip ammoSound;

    private void Start()
    {
        ammoSound = DataManager.Instance.ammoSound;
        collision.onGetAmmo += OnGetAmmoHandler;
    }

    private void OnDestroy()
    {
        collision.onGetAmmo -= OnGetAmmoHandler;
    }

    private void OnGetAmmoHandler()
    {
        photonView.RPC("AmmoSound", RpcTarget.All);
    }

    [PunRPC]
    private void AmmoSound()
    {
        audioSource.PlayOneShot(ammoSound);
    }
}
