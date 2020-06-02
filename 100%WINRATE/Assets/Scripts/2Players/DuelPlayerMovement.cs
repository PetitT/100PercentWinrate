using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerMovement : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerInput input;
    [SerializeField] private DuelPlayerCollision collision;
    [SerializeField] private DuelPlayerManager manager;
    [SerializeField] private GameObject weaponModel;
    [SerializeField] private GameObject bodyModel;
    [SerializeField] private GameObject avatar;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float distanceFromWalls;
    private Camera cam;
    private float baseMoveSpeed;
    private float buffedMoveSpeed;
    private float currentSpeed;
    private float rotationSpeed;
    private float speedBoostDuration;

    private Vector2 movement = Vector2.zero;

    private void Start()
    {
        input.onMoveInput += OnMoveHandler;
        collision.onSpeedBoost += SpeedBoostHandler;
        manager.onReset += OnResetHandler;

        baseMoveSpeed = DuelDataManager.Instance.moveSpeed;
        buffedMoveSpeed = DuelDataManager.Instance.buffedMoveSpeed;
        rotationSpeed = DuelDataManager.Instance.rotationSpeed;
        speedBoostDuration = DuelDataManager.Instance.speedBoostDuration;
        currentSpeed = baseMoveSpeed;
        cam = Camera.main;
    }

    private void OnDestroy()
    {
        input.onMoveInput -= OnMoveHandler;
        collision.onSpeedBoost -= SpeedBoostHandler;
        manager.onReset -= OnResetHandler;
    }

    private void SpeedBoostHandler()
    {
        StartCoroutine("BoostSpeed");
    }

    private void OnMoveHandler(Vector2 movementVector)
    {
        movement = movementVector.normalized;
    }

    private void OnResetHandler()
    {
        StopCoroutine("BoostSpeed");
        currentSpeed = baseMoveSpeed;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            RotateWeapon();
        }
    }

    private IEnumerator BoostSpeed()
    {
        currentSpeed = buffedMoveSpeed;
        yield return new WaitForSeconds(speedBoostDuration);
        currentSpeed = baseMoveSpeed;
    }

    private void Move()
    {
        if (movement != Vector2.zero)
        {
            RotateBody();
        }

        if (CheckWalls())
        {
            avatar.transform.Translate(movement * currentSpeed * Time.deltaTime);
        }
    }

    private void RotateBody()
    {
        Vector3 direction = -movement;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        bodyModel.transform.rotation = Quaternion.Slerp(bodyModel.transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    }

    private bool CheckWalls()
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
}
