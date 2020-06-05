using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class DuelLootSpawner : MonoBehaviourPun
{
    private List<string> loots = new List<string>();
    private GameObject currentLoot = null;
    private bool activeLoot = false;
    private bool canSpawnLoot = false;
    private float timeBetweenLoots;
    private float remainingTimeBetweenLoots;

    private int currentLootIndex;
    private int currentPosIndex;
    private bool isSpawning = false;

    private void Start()
    {
        for (int i = 0; i < DuelDataManager.Instance.ammoLootProbability; i++)
        {
            loots.Add("NetworkAmmo");
        }
        for (int i = 0; i < DuelDataManager.Instance.shieldLootProbability; i++)
        {
            loots.Add("NetworkShield");
        }
        for (int i = 0; i < DuelDataManager.Instance.speedBoostProbability; i++)
        {
            loots.Add("NetworkSpeedBoost");
        }
        DuelGameManager.Instance.onNewRoundStart += OnNewRoundHandler;
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
        timeBetweenLoots = DuelDataManager.Instance.timeBetweenLoots;
        remainingTimeBetweenLoots = timeBetweenLoots;
    }

    private void Update()
    {
        if (isSpawning)
        {
            if (activeLoot)
            {
                if (!currentLoot.activeSelf)
                {
                    activeLoot = false;
                    remainingTimeBetweenLoots = timeBetweenLoots;
                }
            }

            canSpawnLoot = remainingTimeBetweenLoots <= 0;
            if (canSpawnLoot)
            {
                SpawnLoot();
            }
            CountDown();
        }
    }

    private void OnDestroy()
    {
        DuelGameManager.Instance.onNewRoundStart -= OnNewRoundHandler;
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void CountDown()
    {
        remainingTimeBetweenLoots -= Time.deltaTime;
    }

    private void NetworkingClient_EventReceived(ExitGames.Client.Photon.EventData obj)
    {
        if (obj.Code == DuelDataManager.Instance.onGameStartEvent)
        {
            isSpawning = true;
        }
    }

    private void OnNewRoundHandler()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("SpawnLoot", 0.25f);
        }
    }

    private void SpawnLoot()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (activeLoot == false)
            {
                int randomLoot = UnityEngine.Random.Range(0, loots.Count);
                int randomPos = UnityEngine.Random.Range(0, DuelMapManager.Instance.currentMap.lootPositions.Count);
                Vector3 position = DuelMapManager.Instance.currentMap.lootPositions[randomPos].position;

                string itemPath = Path.Combine(StringsManager.Instance.duel, loots[randomLoot]);
                GameObject item = PhotonNetwork.Instantiate(itemPath, position, Quaternion.identity);

                currentLoot = item;
                activeLoot = true;
            }
        }
    }
}
