using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;

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

    public void Initialize(string name, Color imageColor)
    {
        playerName = name;
        image.color = imageColor;
        UpdateScore();
    }

    private void UpdateScore()
    {
        string newText = playerName + " - " + score.ToString();
        text.text = newText;
    }

}
