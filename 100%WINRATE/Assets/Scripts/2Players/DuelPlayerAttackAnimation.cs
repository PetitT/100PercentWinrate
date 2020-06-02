using DG.Tweening;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerAttackAnimation : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerAttack playerAttack;
    [SerializeField] private GameObject weapon;
    [SerializeField] private List<MuzzleFlashInfo> flashInfos;
    [SerializeField] private SpriteRenderer weaponSprite;
    private int spriteView;
    private float muzzleAnimationFrequency;
    private float frequency;

    private void Start()
    {
        playerAttack.onShoot += OnShootHandler;
        frequency = DuelDataManager.Instance.attackFrequency;
        muzzleAnimationFrequency = DataManager.Instance.muzzleAnimFrequency;
        spriteView = weaponSprite.GetComponent<PhotonView>().ViewID;
    }

    private void OnDisable()
    {
        playerAttack.onShoot -= OnShootHandler;
    }

    private void OnShootHandler()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("AnimateWeapon", RpcTarget.All, frequency);
            StartCoroutine(MuzzleFlashAnimation());
        }
    }


    private IEnumerator MuzzleFlashAnimation()
    {
        int random = UnityEngine.Random.Range(0, flashInfos.Count);
        for (int i = 0; i < flashInfos[random].muzzleFlash.Count; i++)
        {
            photonView.RPC("SetMuzzleSprite", RpcTarget.All, spriteView, random, i);
            yield return new WaitForSeconds(muzzleAnimationFrequency);
        }
        photonView.RPC("ClearMuzzleSprite", RpcTarget.AllBuffered, spriteView);
    }

    [PunRPC]
    private void AnimateWeapon(float attackFrequency)
    {
        weapon.transform.DOPunchPosition(-weapon.transform.up * 0.5f, attackFrequency / 2, 1, 0f);
    }

    [PunRPC]
    private void SetMuzzleSprite(int spriteID, int random, int flashNumber)
    {
        SpriteRenderer muzzleSprite = PhotonView.Find(spriteID).GetComponent<SpriteRenderer>();
        muzzleSprite.sprite = flashInfos[random].muzzleFlash[flashNumber];
    }

    [PunRPC]
    private void ClearMuzzleSprite(int spriteID)
    {
        PhotonView.Find(spriteID).GetComponent<SpriteRenderer>().sprite = null;
    }
}
