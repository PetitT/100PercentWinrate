using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PlayerInfo : PunSingleton<PlayerInfo>
{
    [SerializeField] private InputField inputField;
    [SerializeField] private List<string> randomNames;

    public void SetName()
    {
        string name;
        if (inputField.text != "")
        {
            name = inputField.text;
        }
        else
        {
            name = Helper.GetRandomInCollection(randomNames);
        }
        PhotonNetwork.LocalPlayer.NickName = name;
    }
}
