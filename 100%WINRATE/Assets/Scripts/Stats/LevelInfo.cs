using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInfo 
{
    [HideInInspector] public string Name;
    public float health;
    public float damage;
    public float attackSpeed;
    public float damagePerSecond;
    public float healthRegenPerSecond;
    public float timeToFullHealth;
}
