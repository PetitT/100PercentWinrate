using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class GameManager : PunSingleton<GameManager>
{
    [SerializeField] private List<Transform> startPositions;

    public Vector3 GetRandomPosition()
    {
        int random = UnityEngine.Random.Range(0, startPositions.Count);
        return startPositions[random].position;
    }

    private void Start()
    {
        Debug.Log("Create new Player");
        string photonPath = StringsManager.Instance.photon;
        string playerPrefab = StringsManager.Instance.player;
        GameObject player = PhotonNetwork.Instantiate(Path.Combine(photonPath, playerPrefab), GetRandomPosition(), Quaternion.identity);
    }
}
