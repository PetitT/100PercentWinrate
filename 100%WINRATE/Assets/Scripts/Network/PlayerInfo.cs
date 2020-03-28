using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : PunSingleton<PlayerInfo>
{
    [SerializeField] private InputField inputField;
    private PlayerData data;

    public PlayerData Data { get => data; private set => data = value; }

    public void SetName()
    {
        DontDestroyOnLoad(gameObject);
        Data = new PlayerData() { playerName = inputField.text };
        Data.playerName = inputField.text;
    }
}
