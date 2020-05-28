using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerAmmo : MonoBehaviour
{
    [SerializeField] private DuelPlayerAttack attack;
    [SerializeField] private DuelPlayerCollision collision;

    private int maxAmmo;
    private int currentAmmo;

    private void Start()
    {
        attack.onShoot += OnShootHandler;
        collision.onGetAmmo += OnGetAmmoHandler;
        maxAmmo = DuelDataManager.Instance.maxAmmo;
        currentAmmo = maxAmmo;
    }

    private void OnDestroy()
    {
        attack.onShoot -= OnShootHandler;
        collision.onGetAmmo -= OnGetAmmoHandler;
    }

    private void OnGetAmmoHandler()
    {
        ModifyAmmo(1);
    }

    private void OnShootHandler()
    {
        ModifyAmmo(-1);
    }

    private void ModifyAmmo(int amount)
    {
        currentAmmo += amount;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }
}
