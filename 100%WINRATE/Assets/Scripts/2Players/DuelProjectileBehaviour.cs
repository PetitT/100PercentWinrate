using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelProjectileBehaviour : MonoBehaviour
{
    private float remainingLifetime;
    private float speed;

    private void OnEnable()
    {
        remainingLifetime = DuelDataManager.Instance.projectileLifetime;
        speed = DuelDataManager.Instance.projectileSpeed;
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
            Dissapear();
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
        }
        if (collision.CompareTag("Player"))
        {
            Dissapear();
        }
    }

    private void UpdateRotation(Collider2D collision)
    {
        Vector3 newDirection = Vector3.Reflect(transform.up, collision.gameObject.transform.up);
        gameObject.transform.up = newDirection;
    }

    private void Dissapear()
    {
        gameObject.SetActive(false);
    }
}
