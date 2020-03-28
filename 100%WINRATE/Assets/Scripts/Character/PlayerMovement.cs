using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private GameObject avatarModel;
    [SerializeField] private GameObject avatar;
    [SerializeField] private PlayerStatsSetter statsSetter;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float baseDistanceFromWalls;
    private float distanceFromWalls;
    private Camera cam;
    private float speed;

    private void Start()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange += OnStatsChangeHandler;
            playerHealth.onDeath += OnDeathHandler;
            cam = Camera.main;
            Reset();
        }
    }


    private void OnDisable()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange -= OnStatsChangeHandler;
            playerHealth.onDeath -= OnDeathHandler;
        }
    }

    private void OnDeathHandler()
    {
        if (photonView.IsMine)
        {
            Reset();
        }
    }

    private void Reset()
    {
        speed = DataManager.Instance.baseSpeed;
        distanceFromWalls = baseDistanceFromWalls;
    }

    private void OnStatsChangeHandler(Stats stats)
    {
        if (photonView.IsMine)
        {
            speed += stats.movementSpeed;
            distanceFromWalls += stats.bodySize / 2;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            Rotate();
        }
    }

    private void Rotate()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
        mousePosition.x - avatarModel.transform.position.x,
        mousePosition.y - avatarModel.transform.position.y
        );

        avatarModel.transform.up = direction;
    }

    private void Move()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(X, Y).normalized;
        if (CheckWalls(movement))
        {
            avatar.transform.Translate(movement * speed * Time.deltaTime);
        }
    }

    private bool CheckWalls(Vector2 movement)
    {
        Quaternion positive = Quaternion.Euler(0, 0, 45);
        Quaternion negative = Quaternion.Euler(0, 0, -45);
        Vector2 positiveRot = positive * movement;
        Vector2 negativeRot = negative * movement;

        if (Physics2D.Raycast(avatar.transform.position, movement, distanceFromWalls, wallLayer))
        {
            return false;
        }
        else if (Physics2D.Raycast(avatar.transform.position, positiveRot, distanceFromWalls, wallLayer))
        {
            return false;
        }
        else if (Physics2D.Raycast(avatar.transform.position, negativeRot, distanceFromWalls, wallLayer))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
