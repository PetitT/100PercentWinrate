using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviourPun
{
    [SerializeField] private PlayerAttack attack;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerCollision collision;
    [SerializeField] private GameObject avatar;
    [SerializeField] private AudioSource source;
    private int avatarID;
    private AudioClip shootSound;
    private AudioClip hitSound;
    private AudioClip lootSound;

    private void Start()
    {
        attack.onShoot += OnShootHandler;
        collision.onHit += OnHitHandler;
        collision.onLoot += OnLootHandler;
        shootSound = DataManager.Instance.shootSound;
        hitSound = DataManager.Instance.hitSound;
        lootSound = DataManager.Instance.lootSound;
        avatarID = avatar.GetComponent<PhotonView>().ViewID;
    }

    private void OnDisable()
    {
        attack.onShoot -= OnShootHandler;
        collision.onHit -= OnHitHandler;
        collision.onLoot -= OnLootHandler;
    }

    private void OnShootHandler(float obj)
    {
        photonView.RPC("PlayShootSound", RpcTarget.All, avatarID);
    }

    private void OnHitHandler(float obj)
    {
        photonView.RPC("PlayHitSound", RpcTarget.All, avatarID);
    }

    private void OnLootHandler()
    {
        source.PlayOneShot(lootSound);
    }

    [PunRPC]
    private void PlayShootSound(int ID)
    {
        PhotonView.Find(ID).GetComponent<AudioSource>().PlayOneShot(shootSound);
    }

    [PunRPC]
    private void PlayHitSound(int ID)
    {
        PhotonView.Find(ID).GetComponent<AudioSource>().PlayOneShot(hitSound);
    }
}
