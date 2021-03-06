﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.IO;
using DG.Tweening;

public class PlayerAttack : MonoBehaviourPun
{
    [SerializeField] private Transform shootPosition;
    [SerializeField] private PlayerStatsSetter statsSetter;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerBodySkin bodySkin;
    [SerializeField] private PlayerWeaponCollider weaponCollider;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject projectile;

    public event Action<float> onShoot;

    private float attackFrequency;
    private float minAttackFrequency;
    private float remainingAttackFrequency;
    private float projectileSpeed;
    private float projectileSize;
    private float projectileDamage;

    private string prefabPath;
    private string projectilePrefab;

    private bool canShoot = true;
    private Color projectileColor;

    private void Start()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange += StatsChangeHandler;
            playerHealth.onDeath += OnDeathHandler;
            prefabPath = StringsManager.Instance.photon;
            projectilePrefab = StringsManager.Instance.projectile;
            minAttackFrequency = DataManager.Instance.minAttackFrequency;
            projectileColor = bodySkin.PlayerColor;
            ResetDamage();
        }
    }

    private void ResetDamage()
    {
        attackFrequency = DataManager.Instance.baseAttackFrequency;
        projectileSize = DataManager.Instance.baseProjectileSize;
        projectileSpeed = DataManager.Instance.baseProjectileSpeed;
        projectileDamage = DataManager.Instance.baseProjectileDamage;
        remainingAttackFrequency = attackFrequency;
    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange -= StatsChangeHandler;
            playerHealth.onDeath -= OnDeathHandler;
        }
    }

    private void OnDeathHandler()
    {
        if (photonView.IsMine)
        {
            ResetDamage();
        }
    }

    private void StatsChangeHandler(Stats stats)
    {
        if (photonView.IsMine)
        {
            if (attackFrequency <= minAttackFrequency)
            {
                attackFrequency += stats.attackFrequency;
            }
            projectileSpeed += stats.projectileSpeed;
            projectileDamage += stats.projectileDamage;
            projectileSize += stats.projectileSize;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            CoolDown();

            if (canShoot)
            {
                if (!weaponCollider.IsColliding)
                {
                    if (Input.GetMouseButton(0))
                    {
                        Shoot();
                    }
                }
            }
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
        Vector3 currentRotation = shootPosition.transform.rotation.eulerAngles;

        photonView.RPC("SetOfflineProjectileValue", RpcTarget.All,
            projectileSpeed, projectileDamage, photonView.ViewID, projectileSize,
            transform.up.x, transform.up.y,
            shootPosition.position.x, shootPosition.position.y,
            currentRotation.x, currentRotation.y, currentRotation.z,
            projectileColor.r, projectileColor.g, projectileColor.b, projectileColor.a
            );

        onShoot?.Invoke(attackFrequency);

        remainingAttackFrequency = attackFrequency;
        canShoot = false;
    }

    [PunRPC]
    private void SetOfflineProjectileValue(float speed, float damage, int ID, float size, float XDir, float YDir, float XPos, float YPos, float XRot, float YRot, float ZRot, float R, float G, float B, float A)
    {
        GameObject newProjectile = Pool.instance.GetItemFromPool(
            projectile, new Vector2(XPos, YPos), Quaternion.Euler(XRot, YRot, ZRot)
            );

        newProjectile.GetComponent<ProjectileOfflineBehaviour>().Initialize(
            speed, damage, size, new Vector2(XDir, YDir), ID, new Color(R, G, B, A) * DataManager.Instance.colorIntensity
            );
    }
}
