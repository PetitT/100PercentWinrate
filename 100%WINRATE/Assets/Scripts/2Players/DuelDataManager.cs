using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelDataManager : Singleton<DuelDataManager>
{
    [Header("Movement")]
    public float moveSpeed;
    public float buffedMoveSpeed;
    public float rotationSpeed;

    [Header("Attack")]
    public int maxAmmo;
    public float attackFrequency;
    public GameObject projectile;

    [Header("Projectile")]
    public float projectileSpeed;
    public float projectileLifetime;
}
