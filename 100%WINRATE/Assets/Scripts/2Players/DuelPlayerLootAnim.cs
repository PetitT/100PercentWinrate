using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerLootAnim : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerCollision collision;
    [SerializeField] private GameObject avatar;
    private GameObject lootParticle;

    private void Start()
    {
        lootParticle = DataManager.Instance.lootParticle;
        collision.onGetShield += AnimationHandler;
        collision.onSpeedBoost += AnimationHandler;
    }

    private void OnDestroy()
    {
        collision.onGetShield -= AnimationHandler;
        collision.onSpeedBoost -= AnimationHandler;
    }

    private void AnimationHandler()
    {
        photonView.RPC("AnimateLoot", RpcTarget.All, avatar.GetComponent<PhotonView>().ViewID);
    }

    [PunRPC]
    private void AnimateLoot(int playerID)
    {
        GameObject avatar = PhotonView.Find(playerID).gameObject;
        GameObject newParticle = Pool.instance.GetItemFromPool(lootParticle, avatar.transform.position, Quaternion.identity);
        newParticle.transform.SetParent(avatar.transform);
    }
}
