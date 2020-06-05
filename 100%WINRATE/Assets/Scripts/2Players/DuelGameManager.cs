using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Realtime;
using System;
using System.Linq;
using ExitGames.Client.Photon;

public class DuelGameManager : PunSingleton<DuelGameManager>
{
    private Dictionary<int, int> playersIDHealth = new Dictionary<int, int>();
    public Dictionary<int, int> PlayersIDHealth { get => playersIDHealth; private set => playersIDHealth = value; }

    private float roundPause;
    private float timeScaleSlow;

    public event Action onNewRoundStart;
    public event Action onRoundPause;
    public event Action<string> onGameOver;

    private const byte onLostRoundCode = 1;

    private void Start()
    {
        roundPause = DuelDataManager.Instance.roundPause;
        timeScaleSlow = DuelDataManager.Instance.timeSlow;
        PhotonNetwork.NetworkingClient.EventReceived += NetworkEventReceived;
    }

    private void OnDestroy()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkEventReceived;
    }

    public void AddPlayer(int newPlayer, int health)
    {
        photonView.RPC("UpdatePlayers", RpcTarget.AllBuffered, newPlayer, health);
    }

    public void StartGame()
    {
        StartNewRound();
    }

    private void NetworkEventReceived(EventData obj)
    {
        if (obj.Code == DuelDataManager.Instance.onGameStartEvent)
        {
            StartNewRound();
        }

        if (obj.Code == DuelDataManager.Instance.onLostRoundEvent)
        {
            object[] datas = (object[])obj.CustomData;
            int ID = (int)datas[0];
            UpdateScores(ID);
        }
    }

    public void EndGame()
    {
        photonView.RPC("GameOver", RpcTarget.AllBuffered);
    }

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
            StartNewRound();
        }
    }

    [PunRPC]
    private void UpdatePlayers(int newPlayer, int health)
    {
        PlayersIDHealth.Add(newPlayer, health);
    }

    private void StartNewRound()
    {
        MusicManager.Instance.BlendInHighPass();
        onNewRoundStart?.Invoke();
    }

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
