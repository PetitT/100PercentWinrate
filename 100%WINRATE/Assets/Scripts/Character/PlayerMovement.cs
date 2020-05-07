using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private GameObject weaponModel;
    [SerializeField] private GameObject bodyModel;
    [SerializeField] private GameObject avatar;
    [SerializeField] private PlayerStatsSetter statsSetter;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerCollision collision;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float baseDistanceFromWalls;
    private float distanceFromWalls;
    private Camera cam;
    private float targetSpeed;
    private float currentSpeed;
    private float rotationSpeed;

    private void Start()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange += OnStatsChangeHandler;
            playerHealth.onDeath += OnDeathHandler;
            collision.onSlow += OnSlowHandler;
            cam = Camera.main;
            Reset();
        }
    }


    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            statsSetter.onStatsChange -= OnStatsChangeHandler;
            playerHealth.onDeath -= OnDeathHandler;
            collision.onSlow -= OnSlowHandler;
        }
    }

    private void OnDeathHandler()
    {
        if (photonView.IsMine)
        {
            Reset();
        }
    }

    private void OnSlowHandler(bool slow)
    {
        if (slow)
        {
            currentSpeed = targetSpeed * DataManager.Instance.slowObstacleForce / 100;
        }
        else
        {
            currentSpeed = targetSpeed;
        }
    }

    private void Reset()
    {
        targetSpeed = DataManager.Instance.baseMoveSpeed;
        rotationSpeed = DataManager.Instance.rotationSpeed;
        currentSpeed = targetSpeed;
        distanceFromWalls = baseDistanceFromWalls;
    }

    private void OnStatsChangeHandler(Stats stats)
    {
        if (photonView.IsMine)
        {
            targetSpeed += stats.movementSpeed;
            currentSpeed = targetSpeed;
            distanceFromWalls += stats.bodySize / 2;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            RotateWeapon();
        }
    }

    private void RotateWeapon()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
        mousePosition.x - weaponModel.transform.position.x,
        mousePosition.y - weaponModel.transform.position.y
        );

        weaponModel.transform.up = direction;
    }

    private void Move()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(X, Y).normalized;

        if (new Vector2(X, Y) != Vector2.zero)
        {
            RotateBody(movement);
        }

        if (CheckWalls(movement))
        {
            avatar.transform.Translate(movement * currentSpeed * Time.deltaTime);
        }
    }

    private void RotateBody(Vector2 movement)
    {
        Vector3 direction = -movement;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        bodyModel.transform.rotation = Quaternion.Slerp(bodyModel.transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
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
