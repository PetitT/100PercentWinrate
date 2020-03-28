using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.IO;

public class PlayerAttack : MonoBehaviourPun
{
    [SerializeField] private Transform shootPosition;
    [SerializeField] private PlayerStatsSetter statsSetter;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerWeaponCollider weaponCollider;

    private float attackFrequency;
    private float minAttackFrequency;
    private float remainingAttackFrequency;
    private float projectileSpeed;
    private float projectileSize;
    private float projectileDamage;

    private string prefabPath;
    private string projectilePrefab;

    private void Start()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange += StatsChangeHandler;
            playerHealth.onDeath += OnDeathHandler;
            prefabPath = StringsManager.Instance.photon;
            projectilePrefab = StringsManager.Instance.projectile;
            ResetDamage();
        }
    }

    private void ResetDamage()
    {
        remainingAttackFrequency = attackFrequency;
        attackFrequency = DataManager.Instance.baseAttackFrequency;
        projectileSize = DataManager.Instance.baseProjectileSize;
        projectileSpeed = DataManager.Instance.baseProjectileSpeed;
        projectileDamage = DataManager.Instance.baseProjectileDamage;
    }

    private void OnDisable()
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
            if (attackFrequency >= minAttackFrequency)
            {
                attackFrequency -= stats.attackFrequency;
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

            if (remainingAttackFrequency < 0)
            {
                if (!weaponCollider.IsColliding)
                {
                    if (Input.GetMouseButton(0))
                    {
                        GameObject newProjectile = PunPool.Instance.GetItemFromPool(projectilePrefab, shootPosition.position, shootPosition.rotation);
                        photonView.RPC("SetProjectileValues", RpcTarget.AllBuffered, newProjectile.GetComponent<PhotonView>().ViewID, projectileSpeed, projectileDamage, projectileSize);
                        remainingAttackFrequency = attackFrequency;
                    }
                }
            }
        }
    }

    private void CoolDown()
    {
        remainingAttackFrequency -= Time.deltaTime;
    }

    [PunRPC]
    private void SetProjectileValues(int projectileID, float projectileSpeed, float projectileDamage, float projectileSize)
    {
        GameObject projectile = PhotonView.Find(projectileID).gameObject;
        projectile.GetComponent<ProjectileBehaviour>().Initialize(projectileSpeed, projectileDamage);
        projectile.transform.localScale = new Vector2(projectileSize, projectileSize);
    }
}
