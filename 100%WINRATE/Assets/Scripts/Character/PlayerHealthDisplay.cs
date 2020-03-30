using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviourPun
{
    [SerializeField] private GameObject healthBar;
    private float YMaxSize;

    private void Awake()
    {
        YMaxSize = healthBar.transform.localScale.y;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float normalizedHealth = currentHealth / maxHealth;
        float newSize = YMaxSize * normalizedHealth;
        healthBar.transform.localScale = new Vector2(healthBar.transform.localScale.x, newSize);
    }
}
