using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffCheat : MonoBehaviourPun
{
    private char[] charArray;
    private List<string> cheatString = new List<string>();
    private int buffAmount;
    private int cheatIndex = 0;

    public event Action onCheat;

    private void Start()
    {
        charArray = DataManager.Instance.statsBuffCheat.ToCharArray();

        foreach (var item in charArray)
        {
            cheatString.Add(item.ToString());
        }

        buffAmount = DataManager.Instance.statsBuffAmount;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            InputCheat();
        }
    }

    private void InputCheat()
    {
        if (Input.anyKeyDown)
        {
            string input = Input.inputString;
            if (input == cheatString[cheatIndex])
            {
                cheatIndex++;
                if (cheatIndex == charArray.Length)
                {
                    ActivateCheat();
                    cheatIndex = 0;
                }
            }
            else
            {
                cheatIndex = 0;
            }
        }
    }

    private void ActivateCheat()
    {
        for (int i = 0; i < buffAmount; i++)
        {
            onCheat?.Invoke();
        }
    }
}
