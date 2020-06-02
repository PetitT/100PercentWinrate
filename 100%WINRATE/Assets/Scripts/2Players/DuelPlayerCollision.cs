using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerCollision : MonoBehaviourPun
{
    public event Action onHit;
    public event Action onGetShield;
    public event Action onGetAmmo;
    public event Action onSpeedBoost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            if (photonView.IsMine)
            {
                onHit?.Invoke();
            }
        }

        if (collision.CompareTag("Shield"))
        {
            if (photonView.IsMine)
            {
                onGetShield?.Invoke();
            }
            collision.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Ammo"))
        {
            if (photonView.IsMine)
            {
                onGetAmmo?.Invoke();
            }
            collision.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Speed"))
        {
            if (photonView.IsMine)
            {
                onSpeedBoost?.Invoke();
            }
            collision.gameObject.SetActive(false);
        }

    }
}
