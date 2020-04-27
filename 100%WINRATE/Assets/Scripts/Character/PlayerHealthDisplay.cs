using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviourPun
{
    [SerializeField] private SpriteRenderer healthBar;
    private int ID;

    private void Start()
    {
        ID = healthBar.GetComponent<PhotonView>().ViewID;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float normalizedHealth = currentHealth / maxHealth;
        photonView.RPC("DisplayHealth", RpcTarget.AllBuffered, ID, normalizedHealth);
    }

    [PunRPC]
    private void DisplayHealth(int ID, float normalizedHealth)
    {
        Color newColor = new Color(Color.white.r, Color.white.g, Color.white.b, normalizedHealth);
        healthBar.material.SetColor("_BaseColor", newColor);
    }
}
