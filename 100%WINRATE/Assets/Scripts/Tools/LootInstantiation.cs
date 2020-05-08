using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LootInstantiation : MonoBehaviourPun
{
    [SerializeField] private GameObject loot;
    [SerializeField] private Transform minXminY, maxXmaxY;
    private float timeBetweenSpawns;
    private float remainingTimeToSpawn;
    private int spawnsAtStart;

    private void Start()
    {
        timeBetweenSpawns = DataManager.Instance.timeBetweenSpawns;
        remainingTimeToSpawn = timeBetweenSpawns;
        spawnsAtStart = (int)DataManager.Instance.numberOfLootAtStart;

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
        Vector3 pos = GetRandomPosition();
        photonView.RPC("Spawn", RpcTarget.AllBuffered, pos.x, pos.y);
    }

    private Vector3 GetRandomPosition()
    {
        float X = UnityEngine.Random.Range(minXminY.position.x, maxXmaxY.position.x);
        float Y = UnityEngine.Random.Range(minXminY.position.y, maxXmaxY.position.y);
        return new Vector3(X, Y);
    }

    [PunRPC]
    private void Spawn(float X, float Y)
    {
        Pool.instance.GetItemFromPool(loot, new Vector2(X, Y), Quaternion.identity);
    }
}
