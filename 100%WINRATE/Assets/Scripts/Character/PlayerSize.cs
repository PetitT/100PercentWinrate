using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviourPun
{
    [SerializeField] private PlayerStatsSetter statsSetter;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject avatar;
    private Camera cam;
    private float baseCamSize;
    private float currentCamSize;

    private void Start()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange += OnStatsChangeHandler;
            playerHealth.onDeath += OnDeathHandler;
        }

        cam = Camera.main;
        baseCamSize = cam.orthographicSize;
        currentCamSize = baseCamSize;
    }

    private void OnDisable()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange -= OnStatsChangeHandler;
            playerHealth.onDeath += OnDeathHandler;
        }
    }

    private void OnDeathHandler()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("ChangeScale", RpcTarget.AllBuffered, avatar.GetComponent<PhotonView>().ViewID, 1f, 1f);
            cam.orthographicSize = baseCamSize;
        }
    }

    private void OnStatsChangeHandler(Stats stats)
    {
        if (photonView.IsMine)
        {
            float X = avatar.transform.localScale.x + stats.bodySize;
            float Y = avatar.transform.localScale.y + stats.bodySize;
            photonView.RPC("ChangeScale", RpcTarget.AllBuffered, avatar.GetComponent<PhotonView>().ViewID, X, Y);
            cam.orthographicSize += stats.bodySize;
        }
    }

    [PunRPC]
    private void ChangeScale(int playerID, float X, float Y)
    {
        PhotonView.Find(playerID).transform.localScale = new Vector2(X, Y);
    }
}
