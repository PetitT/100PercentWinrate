using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Statinfos")]
public class Statinfos : ScriptableObject
{
    public float baseDamage;
    public float damageBuff;
    public float baseAttackSpeed;
    public float attackSpeedBuff;
    public float minAttackSpeed;
    public float baseHealth;
    public float healthBuff;
    public float baseHealthRegenPerSec;
    public float healthRegenPerSecBuff;
}
