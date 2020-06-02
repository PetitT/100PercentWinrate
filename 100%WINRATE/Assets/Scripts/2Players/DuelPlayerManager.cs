using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerManager : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerHealth health;
    public int playerID { get; private set; }

    public event Action onReset;

    public void SetID(int ID)
    {
        playerID = ID;
    }

    private void Start()
    {
        DuelGameManager.Instance.onNewRoundStart += OnNewRoundHandler;
        health.onDeath += OnDeathHandler;
    }

    private void OnDestroy()
    {
        DuelGameManager.Instance.onNewRoundStart -= OnNewRoundHandler;
        health.onDeath -= OnDeathHandler;
    }

    private void OnDeathHandler()
    {
        DuelGameManager.Instance.LoseRound(photonView.ViewID);
    }

    private void OnNewRoundHandler()
    {
        Invoke("Reset", 0.2f);
    }

    private void Reset()
    {
        onReset?.Invoke();
    }
}
