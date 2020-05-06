using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("Movement")]
    public float baseMoveSpeed;
    public float rotationSpeed;

    [Header("Attack")]
    public float baseAttackFrequency;
    public float baseProjectileSpeed;
    public float baseProjectileSize;
    public float baseProjectileDamage;
    public float minAttackFrequency;
    public float projectileLifetime;
    public float projectileParticleScale;

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

    [Header("DamagedAnimation")]
    [Range(0, 5)] public float chromaticAberrationMaxValue;
    public float damagedAnimationSpeed;
    public float damagedAnimationSpeedRecover;
    public float audioPitchChangeSpeed;
    public float audioPitchMinValue;

    [Header("Sounds")]
    public AudioClip shootSound;
    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip lootSound;

    [Header("Cheats")]
    public string statsBuffCheat;
    public int statsBuffAmount;

    [Header("Other")]
    public float growSpeed;
    public float timeToRespawn;
    public float explosionScaleFactor;
    [Range(0, 100)] public float slowObstacleForce;
    public float muzzleAnimFrequency;
    public float colorIntensity;
    public float lineColorBlendTime;
    public float highPassCutoffOnDeath;
}
