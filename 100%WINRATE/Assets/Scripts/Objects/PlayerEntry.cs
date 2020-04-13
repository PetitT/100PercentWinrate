using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntry : MonoBehaviour
{
    [SerializeField] private Text text;

    private string playerName;
    private int score;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            UpdateScore();
        }
    }

    public void Initialize(string name)
    {
        playerName = name;
        UpdateScore();
    }

    private void UpdateScore()
    {
        string newText = playerName + " - " + score.ToString();
        text.text = newText;
    }

}
