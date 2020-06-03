using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelProjectileBehaviour : MonoBehaviour
{
    private float remainingLifetime;
    private float speed;
    private GameObject ammo;
    private GameObject bounceParticle;
    private GameObject playerHitParticle;

    private void Start()
    {
        ammo = DuelDataManager.Instance.ammo;
        speed = DuelDataManager.Instance.projectileSpeed;
        bounceParticle = DataManager.Instance.wallHitParticle;
        playerHitParticle = DataManager.Instance.playerHitParticle;
    }

    private void OnEnable()
    {
        remainingLifetime = DuelDataManager.Instance.projectileLifetime;
    }

    private void Update()
    {
        Move();
        UpdateLifeTime();
    }

    private void UpdateLifeTime()
    {
        remainingLifetime -= Time.deltaTime;
        if(remainingLifetime <= 0)
        {
            Deactivate(true);
        }
    }

    private void Move()
    {
        gameObject.transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            UpdateRotation(collision);
            Pool.instance.GetItemFromPool(bounceParticle, transform.position, Quaternion.identity);
        }
        if (collision.CompareTag("Player"))
        {
            Pool.instance.GetItemFromPool(playerHitParticle, transform.position, Quaternion.identity);
            Deactivate(false);
        }
    }

    private void UpdateRotation(Collider2D collision)
    {
        Vector3 newDirection = Vector3.Reflect(transform.up, collision.gameObject.transform.up);
        gameObject.transform.up = newDirection;
    }

    private void Deactivate(bool spawnLoot)
    {
        if (spawnLoot)
        {
            Pool.instance.GetItemFromPool(ammo, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
