using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance { get => instance; private set => instance = value; }

    protected virtual void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this as T;
    }
}
