using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.IO;

public class PlayerDeath : MonoBehaviourPun
{
    [SerializeField] private PlayerAttack attack;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerScore score;
    [SerializeField] private GameObject avatar;
    [SerializeField] private GameObject loot;
    [SerializeField] private GameObject explosion;
    private float cutoffFrequencyOnDeath;
    private float explosionScaleFactor;

    private void Start()
    {
        if (photonView.IsMine)
        {
            health.onDeath += OnDeathHandler;
            cutoffFrequencyOnDeath = DataManager.Instance.highPassCutoffOnDeath;
            explosionScaleFactor = DataManager.Instance.explosionScaleFactor;
        }
    }

    private void OnDisable()
    {
        if (photonView.IsMine)
        {
            health.onDeath -= OnDeathHandler;
        }
    }

    private void OnDeathHandler()
    {
        if (photonView.IsMine)
        {
            attack.enabled = false;
            movement.enabled = false;
            MusicManager.Instance.HighPassFilter.cutoffFrequency = cutoffFrequencyOnDeath;
            photonView.RPC("Explode", RpcTarget.All, avatar.GetComponent<PhotonView>().ViewID, score.Score);
            photonView.RPC("SpawnLoot", RpcTarget.AllBuffered, score.Score, avatar.GetComponent<PhotonView>().ViewID);
            photonView.RPC("HideAvatar", RpcTarget.All, avatar.GetComponent<PhotonView>().ViewID);
            StartCoroutine(RespawnTimer());
        }
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(DataManager.Instance.timeToRespawn);
        photonView.RPC("Respawn", RpcTarget.AllBuffered, avatar.GetComponent<PhotonView>().ViewID);
        attack.enabled = true;
        movement.enabled = true;
        MusicManager.Instance.HighPassFilter.cutoffFrequency = 10;
    }

    [PunRPC]
    private void Explode(int playerID, int score)
    {
        Vector2 pos = PhotonView.Find(playerID).gameObject.transform.position;
        GameObject newExplosion = Pool.instance.GetItemFromPool(explosion, pos, Quaternion.identity);

        if (score < 1)
        {
            score = 1;
        }
        newExplosion.transform.localScale = new Vector2(score * DataManager.Instance.bodySizeBuff, score * DataManager.Instance.bodySizeBuff) * explosionScaleFactor;
    }

    [PunRPC]
    private void SpawnLoot(int score, int playerID)
    {
        int numberOfBalls = (score / 2);
        if (numberOfBalls < 1)
        {
            numberOfBalls = 1;
        }
        float sizeBuff = DataManager.Instance.bodySizeBuff;

        GameObject player = PhotonView.Find(playerID).gameObject;
        for (int i = 0; i < numberOfBalls; i++)
        {
            Pool.instance.GetItemFromPool(
            loot,
                new Vector2
                (player.transform.position.x + UnityEngine.Random.Range(sizeBuff * -score, sizeBuff * score),
                player.transform.position.y + UnityEngine.Random.Range(sizeBuff * -score, sizeBuff * score)),
                Quaternion.identity);
        }
    }

    [PunRPC]
    private void HideAvatar(int playerID)
    {
        PhotonView.Find(playerID).gameObject.SetActive(false);
    }

    [PunRPC]
    private void Respawn(int playerID)
    {
        PhotonView.Find(playerID).gameObject.SetActive(true);
        PhotonView.Find(playerID).gameObject.transform.position = GameManager.Instance.GetRandomPosition();
    }
}
