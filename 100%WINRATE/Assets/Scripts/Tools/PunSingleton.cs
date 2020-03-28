using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PunSingleton<T> : MonoBehaviourPun where T : MonoBehaviour
{
    private static T instance;

    public static T Instance { get => instance; private set => instance = value; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this as T;
    }
}
