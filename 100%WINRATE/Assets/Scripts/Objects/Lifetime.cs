using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lifetime : MonoBehaviour
{
    [SerializeField] float lifeTime;
    float remainingLifeTime;
    public UnityEvent onLifeTimeEnd;

    private void OnEnable()
    {
        remainingLifeTime = lifeTime;
    }

    private void Update()
    {
        remainingLifeTime -= Time.deltaTime;
        if (remainingLifeTime <= 0)
        {
            onLifeTimeEnd?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
