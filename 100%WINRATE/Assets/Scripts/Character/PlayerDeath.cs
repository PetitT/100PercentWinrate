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
    private float lootSpawnMultiplicatorOnDeath;
    private float bodySizeBuff;

    private void Start()
    {
        if (photonView.IsMine)
        {
            health.onDeath += OnDeathHandler;
            cutoffFrequencyOnDeath = DataManager.Instance.highPassCutoffOnDeath;
            explosionScaleFactor = DataManager.Instance.explosionScaleFactor;
            lootSpawnMultiplicatorOnDeath = DataManager.Instance.lootSpawnMultiplicatorOnDeath;
            bodySizeBuff = DataManager.Instance.bodySizeBuff;
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
            int currentScore = score.Score;
            photonView.RPC("Explode", RpcTarget.All, avatar.GetComponent<PhotonView>().ViewID, currentScore, explosionScaleFactor, bodySizeBuff);
            SetupSpawns(currentScore);
            photonView.RPC("HideAvatar", RpcTarget.All, avatar.GetComponent<PhotonView>().ViewID);
            StartCoroutine(RespawnTimer());
        }
    }

    private void SetupSpawns(int currentScore)
    {
        int numberOfBalls = (int)(currentScore * lootSpawnMultiplicatorOnDeath);
        if (numberOfBalls < 1)
        {
            numberOfBalls = 1;
        }

        for (int i = 0; i < numberOfBalls; i++)
        {
            float X = UnityEngine.Random.Range(bodySizeBuff * -currentScore, bodySizeBuff * currentScore);
            float Y = UnityEngine.Random.Range(bodySizeBuff * -currentScore, bodySizeBuff * currentScore);
            photonView.RPC("SpawnLoot", RpcTarget.AllBuffered, avatar.GetComponent<PhotonView>().ViewID, X, Y);
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
    private void Explode(int playerID, int score, float scaleFactor, float sizeBuff)
    {
        Vector2 pos = PhotonView.Find(playerID).gameObject.transform.position;
        GameObject newExplosion = Pool.instance.GetItemFromPool(explosion, pos, Quaternion.identity);

        if (score < 1)
        {
            score = 1;
        }
        newExplosion.transform.localScale = new Vector2(score, score) * scaleFactor;
    }

    [PunRPC]
    private void SpawnLoot(int playerID, float X, float Y)
    {
        GameObject player = PhotonView.Find(playerID).gameObject;

        Pool.instance.GetItemFromPool(
        loot,
            new Vector2
            (player.transform.position.x + X,
            player.transform.position.y + Y),
            Quaternion.identity);
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
