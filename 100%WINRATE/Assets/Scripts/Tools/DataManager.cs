using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("Movement")]
    public float baseSpeed;

    [Header("Projectile")]
    public float baseAttackFrequency;
    public float baseProjectileSpeed;
    public float baseProjectileSize;
    public float baseProjectileDamage;
    public float minAttackFrequency;

    [Header("Health")]
    public float baseHealth;
    public float healthRegenValue;
    public float healthRegenFrequency;
    public float minHealthRegenFrequency;

    [Header("Buffs")]
    public float moveSpeedBuff;
    public float AttackFrequencyBuff;
    public float projectileSpeedBuff;
    public float projectileDamageBuff;
    public float projectileSizeBuff;
    public float bodySizeBuff;
    public float healthBuff;
    public float healthRegenValueBuff;
    public float healthRegenFrequencyBuff;

    [Header("LootSpawn")]
    public float timeBetweenSpawns;
    public int numberOfLootAtStart;
}
