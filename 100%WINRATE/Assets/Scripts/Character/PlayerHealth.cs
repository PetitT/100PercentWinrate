using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerHealth : MonoBehaviourPun
{
    [SerializeField] private PlayerHealthDisplay healthDisplay;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private PlayerStatsSetter statsSetter;

    private float currentHealth;
    private float maxHealth;
    private float healthRegenValue;
    private float healthRegenFrequency;
    private float remainingHealthRegenFrequency;
    private float minHealthRegenFrequency;

    public event Action onDeath;

    private void Start()
    {
        if (photonView.IsMine)
        {
            playerCollision.onHit += OnHitHandler;
            statsSetter.onStatsChange += OnStatsChangeHandler;
            ResetHealth();
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Regenerate();
        }
    }

    private void OnDisable()
    {
        if (photonView.IsMine)
        {
            playerCollision.onHit -= OnHitHandler;
            statsSetter.onStatsChange -= OnStatsChangeHandler;
        }
    }

    private void ResetHealth()
    {
        maxHealth = DataManager.Instance.baseHealth;
        currentHealth = maxHealth;
        healthRegenFrequency = DataManager.Instance.healthRegenFrequency;
        remainingHealthRegenFrequency = healthRegenFrequency;
        minHealthRegenFrequency = DataManager.Instance.minHealthRegenFrequency;
        healthRegenValue = DataManager.Instance.healthRegenValue;
        healthDisplay.UpdateHealth(currentHealth);
    }


    private void OnStatsChangeHandler(Stats stats)
    {
        if (photonView.IsMine)
        {
            maxHealth += stats.health;
            currentHealth += stats.health;
            healthRegenValue += stats.healthRegenValue;
            healthRegenFrequency -= stats.healthRegenFrequency;
            if (healthRegenFrequency <= minHealthRegenFrequency)
            {
                healthRegenFrequency = minHealthRegenFrequency;
            }
            healthDisplay.UpdateHealth(currentHealth);
        }
    }

    private void OnHitHandler(float damage)
    {
        if (photonView.IsMine)
        {
            currentHealth -= damage;
            healthDisplay.UpdateHealth(currentHealth);
            if (currentHealth <= 0)
            {
                onDeath?.Invoke();
                ResetHealth();
            }
        }
    }

    private void Regenerate()
    {
        remainingHealthRegenFrequency -= Time.deltaTime;
        if (remainingHealthRegenFrequency <= 0)
        {
            Heal();
            remainingHealthRegenFrequency = healthRegenFrequency;
        }
    }

    private void Heal()
    {
        if (photonView.IsMine)
        {
            currentHealth += healthRegenValue;
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthDisplay.UpdateHealth(currentHealth);
        }
    }
}
