using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DuelLootSpawner : MonoBehaviourPun
{
    private List<GameObject> loots = new List<GameObject>();
    private GameObject currentLoot = null;
    private bool activeLoot = false;

    private void Start()
    {
        for (int i = 0; i < DuelDataManager.Instance.ammoLootPropability; i++)
        {
            loots.Add(DuelDataManager.Instance.ammo);
        }
        for (int i = 0; i < DuelDataManager.Instance.shieldLootProbability; i++)
        {
            loots.Add(DuelDataManager.Instance.shield);
        }
        for (int i = 0; i < DuelDataManager.Instance.speedBoostProbability; i++)
        {
            loots.Add(DuelDataManager.Instance.speedBoost);
        }
        DuelGameManager.Instance.onNewRoundStart += OnNewRoundHandler;
    }

    private void Update()
    {
        if (activeLoot)
        {
            if (!currentLoot.activeSelf)
            {
                activeLoot = false;
            }
        }
    }

    private void OnDestroy()
    {
        DuelGameManager.Instance.onNewRoundStart -= OnNewRoundHandler;
    }

    private void OnNewRoundHandler()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("CreateLoot", 0.25f);
        }
    }

    private void CreateLoot()
    {
        int randomLoot = UnityEngine.Random.Range(0, loots.Count);
        int randomPos = UnityEngine.Random.Range(0, DuelMapManager.Instance.currentMap.lootPositions.Count);
        photonView.RPC("SpawnLoot", RpcTarget.All, randomLoot, randomPos);
    }

    [PunRPC]
    private void SpawnLoot(int loot, int pos)
    {
        if (activeLoot == false)
        {
            GameObject item = Pool.instance.GetItemFromPool(loots[loot], DuelMapManager.Instance.currentMap.lootPositions[pos].position, Quaternion.identity);
            currentLoot = item;
            activeLoot = true;
        }
    }
}
