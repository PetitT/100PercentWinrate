using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodySkin : MonoBehaviourPun
{
    [SerializeField] private SpriteRenderer[] bodySprites;

    private void Start()
    {
        if (photonView.IsMine)
        {
            for (int i = 0; i < bodySprites.Length; i++)
            {
                Color newColor = Random.ColorHSV();
                float a = newColor.a;
                float b = newColor.b;
                float g = newColor.g;
                float r = newColor.r;
                photonView.RPC("SetRandomColor", RpcTarget.AllBuffered, i, r, g, b, a);
            }
        }
    }

    [PunRPC]
    private void SetRandomColor(int spriteIndex, float R, float G, float B, float A)
    {
        bodySprites[spriteIndex].color = new Color(R, G, B, A);
    }
}
