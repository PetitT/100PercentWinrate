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
    private float baseBodySize;
    private float currentCamSize;

    private float baseGrowSpeed;
    private float growSpeed;

    private float targetBodyScale;
    private float targetCamScale;

    private void Start()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange += OnStatsChangeHandler;
            playerHealth.onDeath += OnDeathHandler;
        }

        cam = Camera.main;
        targetCamScale = cam.orthographicSize;
        baseCamSize = cam.orthographicSize;
        baseBodySize = avatar.transform.localScale.x;
        targetBodyScale = avatar.transform.localScale.x;
        baseGrowSpeed = DataManager.Instance.growSpeed;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            GrowBody();
            GrowCamera();
            CheckGrowSpeed();
        }
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
            avatar.transform.localScale = new Vector2(1, 1);
            cam.orthographicSize = baseCamSize;
            targetCamScale = baseCamSize;
            targetBodyScale = baseBodySize;
        }
    }

    private void OnStatsChangeHandler(Stats stats)
    {
        if (photonView.IsMine)
        {
            targetBodyScale += stats.bodySize;
            targetCamScale += stats.bodySize * 1.5f;
        }
    }

    private void GrowBody()
    {
        if (avatar.transform.localScale.x <= targetBodyScale)
        {
            float X = avatar.transform.localScale.x + growSpeed * Time.deltaTime;
            float Y = avatar.transform.localScale.y + growSpeed * Time.deltaTime;

            avatar.transform.localScale = new Vector2(X, Y);
        }
    }

    private void GrowCamera()
    {
        if (cam.orthographicSize <= targetCamScale)
        {
            cam.orthographicSize += growSpeed * Time.deltaTime;
        }
    }

    private void CheckGrowSpeed()
    {
        float statsToGrow = (targetBodyScale - avatar.transform.localScale.x) / DataManager.Instance.bodySizeBuff;
        growSpeed = baseGrowSpeed * statsToGrow/2;
    }

}
