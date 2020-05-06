using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCounter : MonoBehaviour
{
    public Statinfos stats;
    public List<LevelInfo> infos = new List<LevelInfo>();
    public int maxLevel;

    public void GetInfos()
    {
        infos.Clear();
        float damage = stats.baseDamage;
        float attackSpeed = stats.baseAttackSpeed;
        float health = stats.baseHealth;
        float healthRegenPerSecond = stats.baseHealthRegenPerSec;
        float damagePerSecond = GetDPS(stats.baseDamage, stats.baseAttackSpeed);
        for (int i = 0; i < maxLevel; i++)
        {
            if (i != 0)
            {
                damage += stats.damageBuff;
                attackSpeed += stats.attackSpeedBuff;
                if(attackSpeed >= stats.minAttackSpeed)
                {
                    attackSpeed = stats.minAttackSpeed;
                }
                health += stats.healthBuff;
                healthRegenPerSecond += stats.healthRegenPerSecBuff;
            }

            infos.Add(new LevelInfo());
            infos[i].Name = (i + 1).ToString();
            infos[i].damage = damage;
            infos[i].attackSpeed = attackSpeed;
            infos[i].health = health;
            infos[i].healthRegenPerSecond = healthRegenPerSecond;
            infos[i].damagePerSecond = GetDPS(damage, attackSpeed);
            infos[i].timeToFullHealth = GetTimToFullHealth(health, healthRegenPerSecond);
        }
    }

    private float GetTimToFullHealth(float health, float healthRegenPerSecond)
    {
        float timeToFullHealth = health / healthRegenPerSecond;
        return timeToFullHealth;
    }

    private float GetDPS(float damage, float attackSpeed)
    {
        float dps = damage / attackSpeed;
        return dps;
    }
}
