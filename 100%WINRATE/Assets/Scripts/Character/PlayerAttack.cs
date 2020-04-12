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
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject projectile;

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
                        Shoot();
                    }
                }
            }
        }
    }

    private IEnumerator AnimCoolDown()
    {
        yield return new WaitForEndOfFrame();
        photonView.RPC("ToggleAnim", RpcTarget.AllBuffered, false);
    }

    private void CoolDown()
    {
        remainingAttackFrequency -= Time.deltaTime;
    }

    private void Shoot()
    {
        Vector3 currentRotation = shootPosition.transform.rotation.eulerAngles;
        photonView.RPC("SetOfflineProjectileValue", RpcTarget.All,
            projectileSpeed, projectileDamage, photonView.ViewID, projectileSize,
            transform.up.x, transform.up.y,
            shootPosition.position.x, shootPosition.position.y,
            currentRotation.x, currentRotation.y, currentRotation.z
            );
        photonView.RPC("ToggleAnim", RpcTarget.All, true);
        remainingAttackFrequency = attackFrequency;
        StartCoroutine("AnimCoolDown");
    }

    [PunRPC]
    private void SetOfflineProjectileValue(float speed, float damage, int ID, float size, float XDir, float YDir, float XPos, float YPos, float XRot, float YRot, float ZRot)
    {
        GameObject newProjectile = Instantiate(projectile, new Vector2(XPos, YPos), Quaternion.Euler(XRot, YRot, ZRot));
        newProjectile.GetComponent<ProjectileOfflineBehaviour>().Initialize(
            speed, damage, new Vector2(XDir, YDir), ID
            );
        newProjectile.transform.localScale = new Vector2(size, size);
    }

    [PunRPC]
    private void ToggleAnim(bool toggle)
    {
        anim.SetBool("Shoot", toggle);
    }
}
