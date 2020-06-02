using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Realtime;
using System;
using System.Linq;

public class DuelGameManager : PunSingleton<DuelGameManager>
{
    private Dictionary<int, int> playersIDHealth = new Dictionary<int, int>();
    public Dictionary<int, int> PlayersIDHealth { get => playersIDHealth; private set => playersIDHealth = value; }

    public event Action onNewRoundStart;

    public void AddPlayer(int newPlayer, int health)
    {
        photonView.RPC("UpdatePlayers", RpcTarget.AllBuffered, newPlayer, health);
    }

    public void StartGame()
    {
        photonView.RPC("StartNewRound", RpcTarget.AllBuffered);
    }

    public void LoseRound(int ID)
    {
        photonView.RPC("UpdateScores", RpcTarget.All, ID);
    }

    public void EndGame()
    {
        photonView.RPC("GameOver", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void UpdateScores(int ID)
    {
        PlayersIDHealth[ID]--;
        DuelUIManager.Instance.UpdateLives(ID, PlayersIDHealth[ID]);
        if (PlayersIDHealth[ID] <= 0)
        {
            GameOver();
        }
        else
        {
            photonView.RPC("StartNewRound", RpcTarget.All);
        }
    }

    [PunRPC]
    private void UpdatePlayers(int newPlayer, int health)
    {
        PlayersIDHealth.Add(newPlayer, health);
    }

    [PunRPC]
    private void StartNewRound()
    {
        onNewRoundStart?.Invoke();
    }

    [PunRPC]
    private void GameOver()
    {
        Debug.Log("End Game");
    }
}
