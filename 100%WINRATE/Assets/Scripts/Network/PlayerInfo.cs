using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInfo : PunSingleton<PlayerInfo>
{
    [SerializeField] private InputField inputField;

    public void SetName()
    {
        PhotonNetwork.LocalPlayer.NickName = inputField.text;
    }
}
