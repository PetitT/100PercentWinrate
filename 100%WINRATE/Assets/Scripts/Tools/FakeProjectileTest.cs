using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeProjectileTest : MonoBehaviourPun
{
    [SerializeField] private GameObject projectile;

    private void Start()
    {
        InvokeRepeating("Shoot", 0.5f, 1);
    }

    private void Shoot()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;

        FakeShoot(
             1, 1, photonView.ViewID, 1,
             transform.up.x, transform.up.y,
             transform.position.x, transform.position.y,
             currentRotation.x, currentRotation.y, currentRotation.z,
             1, 1, 1, 1
             );
    }

    private void FakeShoot(float speed, float damage, int ID, float size, float XDir, float YDir, float XPos, float YPos, float XRot, float YRot, float ZRot, float R, float G, float B, float A)
    {
        GameObject newProjectile = Pool.instance.GetItemFromPool(
            projectile, new Vector2(XPos, YPos), Quaternion.Euler(XRot, YRot, ZRot)
            );

        newProjectile.GetComponent<ProjectileOfflineBehaviour>().Initialize(
            speed, damage, size, new Vector2(XDir, YDir), ID, new Color(R, G, B, A) * DataManager.Instance.colorIntensity
            );
    }
}
