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
        if (collision.GetComponent<ProjectileBehaviour>() != null)
        {
            if (collision.GetComponent<ProjectileBehaviour>().ID != pv.ViewID)
            {
                if (photonView.IsMine)
                {
                    float damage = collision.GetComponent<ProjectileBehaviour>().Damage;
                    onHit?.Invoke(damage);
                    collision.GetComponent<ProjectileBehaviour>().HideObject();
                    Debug.Log("Hit for " + damage);
                }
            }
        }

        if (collision.GetComponent<LootBehaviour>() != null)
        {
            onLoot?.Invoke();
            collision.GetComponent<LootBehaviour>().HideObject();
        }
    }
}
