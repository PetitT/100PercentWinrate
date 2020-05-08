using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStatsSetter : MonoBehaviourPun
{
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private PlayerBuffCheat cheat;
    private Stats currentStats;
    public event Action<Stats> onStatsChange;

    private void Start()
    {
        if (photonView.IsMine)
        {
            playerCollision.onLoot += OnLootHandler;
            cheat.onCheat += OnLootHandler;
        }
    }

    private void OnDisable()
    {
        if (photonView.IsMine)
        {
            playerCollision.onLoot -= OnLootHandler;
            cheat.onCheat -= OnLootHandler;
        }
    }

    private void OnLootHandler()
    {
        if (photonView.IsMine)
        {
            onStatsChange?.Invoke(new Stats()
            {
                movementSpeed = DataManager.Instance.moveSpeedBuff,
                attackFrequency = DataManager.Instance.attackFrequencyBuff,
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
