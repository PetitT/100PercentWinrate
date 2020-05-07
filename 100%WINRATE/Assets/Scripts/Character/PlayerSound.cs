using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviourPun
{
    [SerializeField] private PlayerCollision collision;
    [SerializeField] private GameObject avatar;
    [SerializeField] private AudioSource source;
    private int avatarID;
    private AudioClip lootSound;

    private void Start()
    {
        collision.onLoot += OnLootHandler;
        lootSound = DataManager.Instance.lootSound;
        avatarID = avatar.GetComponent<PhotonView>().ViewID;
    }

    private void OnDisable()
    {
        collision.onLoot -= OnLootHandler;
    }

    private void OnLootHandler()
    {
        source.PlayOneShot(lootSound);
    }
}
