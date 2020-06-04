using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuelRoundDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro roundText;
    [SerializeField] private Animator anim;
    [SerializeField] private DuelGameManager manager;

    private int currentRound = 0;

    private void Start()
    {
        manager.onNewRoundStart += NewRoundHandler;
        manager.onGameOver += GameOverHandler;
    }

    private void OnDestroy()
    {
        manager.onNewRoundStart -= NewRoundHandler;
        manager.onGameOver -= GameOverHandler;
    }

    private void NewRoundHandler()
    {
        currentRound++;
        roundText.text = "ROUND " + currentRound.ToString();
        anim.SetTrigger("Display");
    }

    private void GameOverHandler(string winnerName)
    {
        roundText.text = "WINNER : " + winnerName;
        anim.SetTrigger("Display");
        anim.SetFloat("Speed", 0.25f);
    }
}
