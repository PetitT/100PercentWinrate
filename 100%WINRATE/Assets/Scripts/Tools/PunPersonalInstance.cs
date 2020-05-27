using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PunPersonalInstance : MonoBehaviourPun
{
    protected virtual void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }
    }
}
