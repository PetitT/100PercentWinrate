using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DuelMapManager : PunSingleton<DuelMapManager>
{
    [SerializeField] private List<MapInfo> maps;
    public MapInfo currentMap { get; private set; } = null;

    private void Start()
    {
        DuelGameManager.Instance.onNewRoundStart += OnNewRoundHandler;
        currentMap = maps[0];
    }

    private void OnDestroy()
    {
        DuelGameManager.Instance.onNewRoundStart -= OnNewRoundHandler;
    }

    private void OnNewRoundHandler()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int random = UnityEngine.Random.Range(0, maps.Count);
            photonView.RPC("PickNewMap", RpcTarget.All, random);
        }
    }

    [PunRPC]
    private void PickNewMap(int random)
    {
        currentMap.map.SetActive(false);
        currentMap = maps[random];
        currentMap.map.SetActive(true);
    }
}
