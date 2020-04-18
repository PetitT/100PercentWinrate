using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodySkin : MonoBehaviourPun
{
    [SerializeField] private SpriteRenderer[] bodySprites;
    private Color playerColor;

    public Color PlayerColor { get => playerColor; private set => playerColor = value; }

    private void Awake()
    {
        playerColor = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        FindObjectOfType<PlayerList>().playerColor = playerColor * DataManager.Instance.colorIntensity;
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            for (int i = 0; i < bodySprites.Length; i++)
            {
                float r = playerColor.r;
                float g = playerColor.g;
                float b = playerColor.b;
                float a = playerColor.a;
                photonView.RPC("SetRandomColor", RpcTarget.AllBuffered, i, r, g, b, a);
            }
        }
    }

    [PunRPC]
    private void SetRandomColor(int spriteIndex, float R, float G, float B, float A)
    {
        bodySprites[spriteIndex].material.SetColor("Glow_Color", new Color(R, G, B, A) * DataManager.Instance.colorIntensity);
    }
}
