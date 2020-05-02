using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOfflineBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject playerHitParticle;
    [SerializeField] private GameObject obstacleHitParticle;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer trail;
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
        trail.widthMultiplier = size/3;
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
            Disable(obstacleHitParticle, transform.position);
        }
    }

    private void Disable(GameObject particle, Vector3 position)
    {
        GameObject newParticle = Pool.instance.GetItemFromPool(particle, transform.position, Quaternion.identity);
        newParticle.transform.localScale = transform.localScale * projectileHitParticleScale;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Vector3 collisionPoint = collision.bounds.ClosestPoint(transform.position);
            Disable(obstacleHitParticle, collisionPoint);
        }

        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponentInParent<PlayerAttack>().GetComponent<PhotonView>().ViewID != ID)
            {
                Vector3 collisionPoint = collision.bounds.ClosestPoint(transform.position);
                Disable(playerHitParticle, collisionPoint);
            }
        }
    }
}
