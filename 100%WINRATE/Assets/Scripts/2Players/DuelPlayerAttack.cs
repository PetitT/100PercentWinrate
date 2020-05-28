using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DuelPlayerAttack : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerInput playerInput;
    [SerializeField] private DuelPlayerAmmo ammo;
    [SerializeField] private PlayerWeaponCollider weaponCollider;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject weapon;

    private float attackFrequency;
    private float remainingAttackFrequency;

    private bool canShoot = true;

    public event Action onShoot;

    private void Start()
    {
        playerInput.onShootInput += OnShootHandler;
        attackFrequency = DuelDataManager.Instance.attackFrequency;
    }

    private void OnDestroy()
    {
        playerInput.onShootInput -= OnShootHandler;
    }

    private void OnShootHandler()
    {
        if (canShoot && !weaponCollider.IsColliding && ammo.HasAmmo())
        {
            Shoot();
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            CoolDown();
        }
    }

    private void CoolDown()
    {
        remainingAttackFrequency -= Time.deltaTime;
        if (remainingAttackFrequency <= 0)
        {
            canShoot = true;
        }
    }

    private void Shoot()
    {
        Vector3 shootRotation = shootPosition.transform.rotation.eulerAngles;

        photonView.RPC("FireProjectile", RpcTarget.All,
            shootRotation.x, shootRotation.y, shootRotation.z,
            shootPosition.transform.position.x, shootPosition.transform.position.y);

        onShoot?.Invoke();
        canShoot = false;
        remainingAttackFrequency = attackFrequency;
    }

    [PunRPC]
    private void FireProjectile(float Xrot, float Yrot, float ZRot, float Xpos, float YPos)
    {
        GameObject newProjectile = Pool.instance.GetItemFromPool(
            DuelDataManager.Instance.projectile,
            new Vector2(Xpos, YPos),
            Quaternion.Euler(Xrot, Yrot, ZRot)
            );
    }
}
