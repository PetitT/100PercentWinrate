using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnNewRound : MonoBehaviour
{
    private void Start()
    {
        DuelGameManager.Instance.onNewRoundStart += OnNewRoundHandler;
    }

    private void OnDestroy()
    {
        DuelGameManager.Instance.onNewRoundStart -= OnNewRoundHandler;
    }

    private void OnNewRoundHandler()
    {
        gameObject.SetActive(false);
    }
}
