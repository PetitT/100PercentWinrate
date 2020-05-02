using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthDisplay : MonoBehaviourPun
{
    [SerializeField] private SpriteRenderer healthBar;
    [SerializeField] private TextMeshPro healthNumber;
    private int ID;

    private void Start()
    {
        ID = healthBar.GetComponent<PhotonView>().ViewID;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float normalizedHealth = currentHealth / maxHealth;
        photonView.RPC("DisplayHealth", RpcTarget.AllBuffered, ID, normalizedHealth, (int)currentHealth);
    }

    [PunRPC]
    private void DisplayHealth(int ID, float normalizedHealth, int currenthealth)
    {
        Color newColor = new Color(Color.white.r, Color.white.g, Color.white.b, normalizedHealth);
        healthBar.material.SetColor("_BaseColor", newColor);
        healthNumber.text = currenthealth.ToString();
    }
}
