using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehaviour : MonoBehaviourPun
{
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
