using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLootAnimation : MonoBehaviourPun
{
    [SerializeField] private PlayerCollision collision;
    [SerializeField] private GameObject lootParticle;
    [SerializeField] private GameObject avatar;

    private void Start()
    {
        collision.onLoot += OnLootHandler;
    }

    private void OnDisable()
    {
        collision.onLoot -= OnLootHandler;
    }

    private void OnLootHandler()
    {
        photonView.RPC("AnimateLoot", RpcTarget.All, avatar.GetComponent<PhotonView>().ViewID);
    }

    [PunRPC]
    private void AnimateLoot(int playerID)
    {
        GameObject avatar = PhotonView.Find(playerID).gameObject;
        GameObject newParticle = Pool.instance.GetItemFromPool(lootParticle, avatar.transform.position, Quaternion.identity);
        newParticle.transform.localScale = avatar.transform.localScale;
        newParticle.transform.SetParent(avatar.transform);
    }
}
