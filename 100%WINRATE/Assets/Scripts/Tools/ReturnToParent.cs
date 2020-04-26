using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToParent : MonoBehaviour
{
    private GameObject poolParent;

    private void Start()
    {
        poolParent = GameObject.Find(StringsManager.Instance.poolParent);
    }

    public void Return()
    {
        transform.SetParent(poolParent.transform);
    }
}
