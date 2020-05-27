using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DuelPlayerAttack : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerInput playerInput;
    [SerializeField] private PlayerWeaponCollider weaponCollider;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject weapon;

    private int maxAmmo;
    private int currentAmmo;

    private float attackFrequency;
    private float remainingAttackFrequency;

    private bool canShoot = true;

    public event Action onShoot;

    private void Start()
    {
        playerInput.onShoot += OnShootHandler;
        attackFrequency = DuelDataManager.Instance.attackFrequency;
        maxAmmo = DuelDataManager.Instance.maxAmmo;
        currentAmmo = maxAmmo;
    }

    private void OnDestroy()
    {
        playerInput.onShoot -= OnShootHandler;
    }

    private void OnShootHandler()
    {
        if (canShoot && !weaponCollider.IsColliding && currentAmmo > 0)
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
        currentAmmo--;
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
