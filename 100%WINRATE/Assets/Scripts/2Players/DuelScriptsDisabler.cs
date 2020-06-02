using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelScriptsDisabler : MonoBehaviourPun
{
    [SerializeField] private List<MonoBehaviour> behaviours;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                Destroy(behaviours[i]);
                //behaviours[i].enabled = false;
            }
        }
    }
}
