using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviourPun
{
    private Camera cam;
    [SerializeField] private GameObject avatar;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Vector2 position = avatar.transform.position;
            cam.transform.position = new Vector3(position.x, position.y, -5);
        }
    }
}
