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

    private float roundPause;
    private float timeScaleSlow;

    public event Action onNewRoundStart;
    public event Action onRoundPause;
    public event Action<string> onGameOver;

    private void Start()
    {
        roundPause = DuelDataManager.Instance.roundPause;
        timeScaleSlow = DuelDataManager.Instance.timeSlow;
    }

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
        onRoundPause?.Invoke();
        PlayersIDHealth[ID]--;
        DuelUIManager.Instance.UpdateLives(ID, PlayersIDHealth[ID]);
        StartCoroutine(RoundPauseWait(ID));
    }

    private IEnumerator RoundPauseWait(int ID)
    {
        Time.timeScale = timeScaleSlow;
        yield return new WaitForSecondsRealtime(roundPause);
        Time.timeScale = 1;

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
        MusicManager.Instance.BlendInHighPass();
        onNewRoundStart?.Invoke();
    }

    [PunRPC]
    private void GameOver()
    {
        int winnerID = playersIDHealth.Where(player => player.Value != 0).FirstOrDefault().Key;
        Player winner = PhotonView.Find(winnerID).Owner;
        onGameOver?.Invoke(winner.NickName);
        StartCoroutine("Finish");
    }

    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("LogScene");
    }
}
