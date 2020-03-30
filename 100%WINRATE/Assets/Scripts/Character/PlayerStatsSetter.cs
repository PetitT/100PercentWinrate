using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStatsSetter : MonoBehaviourPun
{
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private PlayerBuffCheat buffCheat;
    private Stats currentStats;
    public event Action<Stats> onStatsChange;

    private void Start()
    {
        if (photonView.IsMine)
        {
            playerCollision.onLoot += OnLootHandler;
            buffCheat.onCheat += OnLootHandler;
        }
    }

    private void OnDisable()
    {
        if (photonView.IsMine)
        {
            playerCollision.onLoot -= OnLootHandler;
            buffCheat.onCheat -= OnLootHandler;
        }
    }

    private void OnLootHandler()
    {
        if (photonView.IsMine)
        {
            onStatsChange?.Invoke(new Stats()
            {
                movementSpeed = DataManager.Instance.moveSpeedBuff,
                attackFrequency = DataManager.Instance.AttackFrequencyBuff,
                bodySize = DataManager.Instance.bodySizeBuff,
                projectileDamage = DataManager.Instance.projectileDamageBuff,
                projectileSpeed = DataManager.Instance.projectileSpeedBuff,
                projectileSize = DataManager.Instance.projectileSizeBuff,
                health = DataManager.Instance.healthBuff,
                healthRegenFrequency = DataManager.Instance.healthRegenFrequencyBuff,
                healthRegenValue = DataManager.Instance.healthRegenValue
            });
        }
    }
}
