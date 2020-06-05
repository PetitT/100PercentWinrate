using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelDataManager : Singleton<DuelDataManager>
{
    [Header("Lives")]
    public int lives;

    [Header("Movement")]
    public float moveSpeed;
    public float buffedMoveSpeed;
    public float speedBoostDuration;
    public float rotationSpeed;

    [Header("Attack")]
    public int maxAmmo;
    public int startAmmo;
    public float attackFrequency;
    public GameObject projectile;

    [Header("Projectile")]
    public float projectileSpeed;
    public float projectileLifetime;

    [Header("Loot")]
    public GameObject ammo;
    public GameObject shield;
    public GameObject speedBoost;
    public int ammoLootProbability;
    public int shieldLootProbability;
    public int speedBoostProbability;
    public float timeBetweenLoots;

    [Header("UI")]
    public GameObject redHeart;
    public GameObject blueHeart;
    public GameObject ammoUI;

    [Header("Others")]
    public float roundPause;
    public float timeSlow;
    public float boundsTeleportSecurityDistance;

    [Header("EventBytes")]
    public byte onGameStartEvent;
    public byte onLostRoundEvent;
}
