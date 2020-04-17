using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOfflineBehaviour : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer trail;
    private float remainingLifeTime;
    private float speed;
    private Vector2 rotation;
    private float damage;
    private int id;

    public float Damage { get => damage; private set => damage = value; }
    public int ID { get => id; private set => id = value; }

    public void Initialize(float projectileSpeed, float projectileDamage, float size, Vector2 projectileRotation, int shooterID, Color projectileColor)
    {
        speed = projectileSpeed;
        Damage = projectileDamage;
        ID = shooterID;
        rotation = projectileRotation;
        remainingLifeTime = lifeTime;
        transform.localScale = new Vector2(size, size);
        spriteRenderer.color = projectileColor;
        trail.Clear();
        trail.material.color = projectileColor;
        trail.material.SetColor("_EmissionColor", projectileColor);
        trail.widthMultiplier = size;
    }

    private void Update()
    {
        Move();
        LifeTime();
    }

    private void Move()
    {
        gameObject.transform.Translate(rotation * speed * Time.deltaTime);
    }

    private void LifeTime()
    {
        remainingLifeTime -= Time.deltaTime;
        if (remainingLifeTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponentInParent<PlayerAttack>().GetComponent<PhotonView>().ViewID != ID)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
