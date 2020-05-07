using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviourPun
{
    [SerializeField] private PlayerStatsSetter statsSetter;
    [SerializeField] private PlayerHealth playerHealth;
    private int score;

    public int Score { get => score; private set => score = value; }

    private void Start()
    {
        statsSetter.onStatsChange += OnStatsChangeHandler;
        playerHealth.onDeath += OnDeathHandler;
    }

    private void OnDisable()
    {
        statsSetter.onStatsChange -= OnStatsChangeHandler;
        playerHealth.onDeath += OnDeathHandler;
    }

    private void OnDeathHandler()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("ResetScore", RpcTarget.AllBuffered);
            PlayerList.instance.UpdateList(PhotonNetwork.LocalPlayer, 0);
        }
    }

    private void OnStatsChangeHandler(Stats obj)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("AddScore", RpcTarget.AllBuffered);
            PlayerList.instance.UpdateList(PhotonNetwork.LocalPlayer, score);
        }
    }

    [PunRPC]
    private void AddScore()
    {
        score++;
    }

    [PunRPC]
    private void ResetScore()
    {
        score = 0;
    }

}
