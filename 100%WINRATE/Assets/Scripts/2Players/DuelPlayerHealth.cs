using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerHealth : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerManager manager;
    [SerializeField] private DuelPlayerCollision collision;
    [SerializeField] private GameObject shield;
    private bool isShielded = false;
    public event Action onDeath;
    public event Action onLostShield;

    private void Start()
    {
        collision.onHit += OnHitHandler;
        collision.onGetShield += OnShieldHandler;
        manager.onReset += OnResetHandler;
    }

    private void OnDestroy()
    {
        collision.onHit -= OnHitHandler;
        collision.onGetShield -= OnShieldHandler;
        manager.onReset -= OnResetHandler;
    }

    private void OnHitHandler()
    {
        if (isShielded)
        {
            LoseShield();
        }
        else
        {
            onDeath?.Invoke();
        }
    }

    private void OnShieldHandler()
    {
        isShielded = true;
        photonView.RPC("ToggleShield", RpcTarget.All, true);
    }

    private void OnResetHandler()
    {
        LoseShield();
    }

    private void LoseShield()
    {
        isShielded = false;
        photonView.RPC("ToggleShield", RpcTarget.All, false);
        onLostShield?.Invoke();
    }

    [PunRPC]
    private void ToggleShield(bool toggle)
    {
        shield.SetActive(toggle);
    }
}
