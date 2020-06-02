using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerAmmo : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerAttack attack;
    [SerializeField] private DuelPlayerCollision collision;
    [SerializeField] private DuelPlayerManager manager;
    [SerializeField] private DuelAmmoDisplay ammoDisplay;

    private int maxAmmo;
    private int startAmmo;
    private int currentAmmo;

    private void Start()
    {
        attack.onShoot += OnShootHandler;
        collision.onGetAmmo += OnGetAmmoHandler;
        manager.onReset += OnResetHandler;
        maxAmmo = DuelDataManager.Instance.maxAmmo;
        startAmmo = DuelDataManager.Instance.startAmmo;
        OnResetHandler();
    }

    private void OnDestroy()
    {
        attack.onShoot -= OnShootHandler;
        collision.onGetAmmo -= OnGetAmmoHandler;
        manager.onReset -= OnResetHandler;
    }

    private void OnGetAmmoHandler()
    {
        ModifyAmmo(1);
    }

    private void OnShootHandler()
    {
        ModifyAmmo(-1);
    }

    private void OnResetHandler()
    {
        currentAmmo = startAmmo;
        ammoDisplay.DisplayAmmo(currentAmmo);
    }

    private void ModifyAmmo(int amount)
    {
        currentAmmo += amount;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        ammoDisplay.DisplayAmmo(currentAmmo);
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }
}
