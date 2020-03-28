using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCollision : MonoBehaviourPun
{
    public event Action<float> onHit;
    public event Action onLoot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.GetComponent<ProjectileBehaviour>() != null)
            {
                float damage = collision.GetComponent<ProjectileBehaviour>().Damage;
                onHit?.Invoke(damage);
                collision.GetComponent<ProjectileBehaviour>().HideObject();
            }

            if (collision.GetComponent<LootBehaviour>() != null)
            {
                onLoot?.Invoke();
                collision.GetComponent<LootBehaviour>().HideObject();
            }
        }
    }
}
