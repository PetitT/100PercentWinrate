using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.IO;

public class PlayerDeath : MonoBehaviourPun
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerScore score;
    [SerializeField] private GameObject avatar;

    private void Start()
    {
        health.onDeath += OnDeathHandler;
    }

    private void OnDisable()
    {
        health.onDeath -= OnDeathHandler;
    }

    private void OnDeathHandler()
    {
        photonView.RPC("SpawnLoot", RpcTarget.AllBuffered, score.Score, avatar.GetComponent<PhotonView>().ViewID);
        photonView.RPC("Respawn", RpcTarget.AllBuffered, avatar.GetComponent<PhotonView>().ViewID);
    }

    [PunRPC]
    private void SpawnLoot(int score, int playerID)
    {
        Bounds spriteBounds = PhotonView.Find(playerID).GetComponentInChildren<SpriteRenderer>().sprite.bounds;
        GameObject player = PhotonView.Find(playerID).gameObject;
        for (int i = 0; i < score + 1; i++)
        {
            PunPool.Instance.GetItemFromPool(
                StringsManager.Instance.loot,
                new Vector2(
                    player.transform.position.x + (UnityEngine.Random.Range(spriteBounds.min.x, spriteBounds.max.x) * 5),
                    player.transform.position.y + (UnityEngine.Random.Range(spriteBounds.min.y, spriteBounds.max.y) * 5)),
                Quaternion.identity
                );
        }
    }

    [PunRPC]
    private void Respawn(int playerID)
    {
        PhotonView.Find(playerID).gameObject.transform.position = GameManager.Instance.GetRandomPosition();
    }
}
