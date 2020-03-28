using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviourPun
{
    [SerializeField] private GameObject healthBar;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float normalizedHealth = currentHealth / maxHealth;
        healthBar.transform.localScale = new Vector2(normalizedHealth, 1);
    }
}
