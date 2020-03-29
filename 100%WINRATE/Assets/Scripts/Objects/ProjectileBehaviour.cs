using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProjectileBehaviour : MonoBehaviourPun
{
    [SerializeField] private float lifeTime;
    private float remainingLifeTime;
    private float speed;
    private float damage;
    private int id;

    public float Damage { get => damage; private set => damage = value; }
    public int ID { get => id; private set => id = value; }

    private void OnEnable()
    {
        remainingLifeTime = lifeTime;
    }

    public void Initialize(float projectileSpeed, float projectileDamage, int shooterID)
    {
        speed = projectileSpeed;
        Damage = projectileDamage;
        ID = shooterID;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            LifeTime();
        }
    }

    private void Move()
    {
        gameObject.transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void LifeTime()
    {
        remainingLifeTime -= Time.deltaTime;
        if (remainingLifeTime <= 0)
        {
            HideObject();
        }
    }

    public void HideObject()
    {
        photonView.RPC("Hide", RpcTarget.AllBuffered, photonView.ViewID);
    }

    [PunRPC]
    private void Hide(int ID)
    {
        PhotonView.Find(ID).gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            photonView.RPC("Hide", RpcTarget.AllBuffered, photonView.ViewID);
        }
    }

}
