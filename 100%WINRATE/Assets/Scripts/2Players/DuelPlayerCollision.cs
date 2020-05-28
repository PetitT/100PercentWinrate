using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerCollision : MonoBehaviour
{
    public event Action onHit;
    public event Action onGetShield;
    public event Action onGetAmmo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            onHit?.Invoke();
        }

        if (collision.CompareTag("Shield"))
        {
            onGetShield?.Invoke();
        }

        if (collision.CompareTag("Ammo"))
        {
            onGetAmmo?.Invoke();
        }
    }
}
