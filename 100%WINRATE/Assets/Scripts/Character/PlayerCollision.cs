using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviourPun
{
    [SerializeField] private PhotonView pv;
    public event Action<float> onHit;
    public event Action onLoot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.CompareTag("Projectile"))
            {
                if (collision.GetComponent<ProjectileOfflineBehaviour>().ID != pv.ViewID)
                {
                    float damage = collision.GetComponent<ProjectileOfflineBehaviour>().Damage;
                    onHit?.Invoke(damage);
                    Debug.Log("Hit for " + damage);
                }
            }

            if (collision.CompareTag("Loot"))
            {
                onLoot?.Invoke();
                collision.GetComponent<LootBehaviour>().HideObject();
            }
        }
    }
}
