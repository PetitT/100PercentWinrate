using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerHealth : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerCollision collision;
    private bool isShielded = false;
    public event Action onDeath;
    public event Action onLostShield;

    private void Start()
    {
        collision.onHit += OnHitHandler;
        collision.onGetShield += OnShieldHandler;
    }

    private void OnDestroy()
    {
        collision.onHit -= OnHitHandler;
        collision.onGetShield -= OnShieldHandler;
    }

    private void OnHitHandler()
    {
        if (isShielded)
        {
            isShielded = false;
            onLostShield?.Invoke();
        }
        else
        {
            onDeath?.Invoke();
        }
    }

    private void OnShieldHandler()
    {
        isShielded = true;
    }
}
