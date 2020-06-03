using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DuelPlayerReposition : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerManager playerManager;
    [SerializeField] private GameObject avatar;
    [SerializeField] private List<TrailRenderer> trails;

    private void Start()
    {
        playerManager.onReset += OnResetHandler;
        OnResetHandler();
    }

    private void OnDestroy()
    {
        playerManager.onReset -= OnResetHandler;
    }

    private void OnResetHandler()
    {
        photonView.RPC("ToggleTrails", RpcTarget.All, false);
        StartCoroutine("Pause");
        int ID = playerManager.playerID;
        Transform newPosition = DuelMapManager.Instance.currentMap.startPositions[ID];
        avatar.transform.position = newPosition.position;
        photonView.RPC("ToggleTrails", RpcTarget.All, true);
    }

    private IEnumerator Pause()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1f;
    }

    [PunRPC]
    private void ToggleTrails(bool toggle)
    {
        for (int i = 0; i < trails.Count; i++)
        {
            trails[i].Clear();
        }
    }
}
