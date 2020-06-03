using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerManager : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerHealth health;
    [SerializeField] private DuelPlayerInput input;
    [SerializeField] private BoxCollider2D collision;
    [SerializeField] private GameObject body;
    public int playerID { get; private set; }

    public event Action onReset;

    public void SetID(int ID)
    {
        playerID = ID;
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            body.layer = 0;
        }
        DuelGameManager.Instance.onNewRoundStart += OnNewRoundHandler;
        DuelGameManager.Instance.onRoundPause += OnRoundPauseHandler;
        health.onDeath += OnDeathHandler;
    }

    private void OnDestroy()
    {
        DuelGameManager.Instance.onNewRoundStart -= OnNewRoundHandler;
        DuelGameManager.Instance.onRoundPause -= OnRoundPauseHandler;
        health.onDeath -= OnDeathHandler;
    }

    private void OnRoundPauseHandler()
    {
        collision.enabled = false;
    }

    private void OnDeathHandler()
    {
        DuelGameManager.Instance.LoseRound(photonView.ViewID);
    }

    private void OnNewRoundHandler()
    {
        Invoke("Reset", 0.01f);
    }

    private void Reset()
    {
        onReset?.Invoke();
        collision.enabled = true;
        input.ToggleInputs(true);
    }
}
