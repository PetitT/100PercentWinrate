using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LootInstantiation : MonoBehaviourPun
{
    [SerializeField] private Transform minXminY, maxXmaxY;
    private float timeBetweenSpawns;
    private float remainingTimeToSpawn;
    private int spawnsAtStart;

    private void Start()
    {
        timeBetweenSpawns = DataManager.Instance.timeBetweenSpawns;
        remainingTimeToSpawn = timeBetweenSpawns;
        spawnsAtStart = DataManager.Instance.numberOfLootAtStart;

        for (int i = 0; i < spawnsAtStart; i++)
        {
            SpawnItem();
        }
    }

    private void Update()
    {
        remainingTimeToSpawn -= Time.deltaTime;
        if(remainingTimeToSpawn <= 0)
        {
            SpawnItem();
            remainingTimeToSpawn = timeBetweenSpawns;
        }
    }

    private void SpawnItem()
    {
        PunPool.Instance.GetItemFromPool(StringsManager.Instance.loot, GetRandomPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        float X = UnityEngine.Random.Range(minXminY.position.x, maxXmaxY.position.x);
        float Y = UnityEngine.Random.Range(minXminY.position.y, maxXmaxY.position.y);
        return new Vector3(X, Y);
    }
}
