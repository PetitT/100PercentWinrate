using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOfflineBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer trail;
    private GameObject playerHitParticle;
    private GameObject obstacleHitParticle;
    private float projectileHitParticleScale;
    private float lifeTime;
    private float remainingLifeTime;
    private float speed;
    private Vector2 rotation;
    private float damage;
    private int id;

    public float Damage { get => damage; private set => damage = value; }
    public int ID { get => id; private set => id = value; }

    public void Initialize(float projectileSpeed, float projectileDamage, float size, Vector2 projectileRotation, int shooterID, Color projectileColor)
    {
        playerHitParticle = DataManager.Instance.playerHitParticle;
        obstacleHitParticle = DataManager.Instance.wallHitParticle;
        speed = projectileSpeed;
        Damage = projectileDamage;
        ID = shooterID;
        rotation = projectileRotation;
        remainingLifeTime = lifeTime;
        transform.localScale = new Vector2(size, size);
        spriteRenderer.color = projectileColor;
        trail.Clear();
        trail.material.color = projectileColor;
        trail.material.SetColor("_Color", projectileColor);
        trail.material.SetColor("_EmissionColor", projectileColor);
        trail.widthMultiplier = size / 3;
    }

    private void OnEnable()
    {
        lifeTime = DataManager.Instance.projectileLifetime;
        projectileHitParticleScale = DataManager.Instance.projectileParticleScale;
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

    private void SpawnParticle(GameObject particle, Vector3 position)
    {
        GameObject newParticle = Pool.instance.GetItemFromPool(particle, transform.position, Quaternion.identity);
        newParticle.transform.localScale = transform.localScale * projectileHitParticleScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Vector3 collisionPoint = collision.bounds.ClosestPoint(transform.position);
            SpawnParticle(obstacleHitParticle, collisionPoint);
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponentInParent<PlayerAttack>().GetComponent<PhotonView>().ViewID != ID)
            {
                Vector3 collisionPoint = collision.bounds.ClosestPoint(transform.position);
                SpawnParticle(playerHitParticle, collisionPoint);
                gameObject.SetActive(false);
            }
        }
    }
}
