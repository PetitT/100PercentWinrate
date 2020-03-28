using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameManager : PunSingleton<GameManager>
{
    [SerializeField] private List<Transform> startPositions;

    private void Start()
    {
        Debug.Log("Create new Player");
        string photonPath = StringsManager.Instance.photon;
        string playerPrefab = StringsManager.Instance.player;
        PhotonNetwork.Instantiate(Path.Combine(photonPath, playerPrefab), GetRandomPosition(), Quaternion.identity);
    }

    public Vector3 GetRandomPosition()
    {
        int random = UnityEngine.Random.Range(0, startPositions.Count);
        return startPositions[random].position;
    }
}
