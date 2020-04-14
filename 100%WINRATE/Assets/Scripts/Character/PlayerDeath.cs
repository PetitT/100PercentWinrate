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
    [SerializeField] private GameObject loot;
    [SerializeField] private GameObject explosion;

    private void Start()
    {
        if (photonView.IsMine)
        {
            health.onDeath += OnDeathHandler;
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
    }

    [PunRPC]
    private void Explode(int playerID, int score)
    {
        Vector2 pos = PhotonView.Find(playerID).gameObject.transform.position;
        Pool.instance.GetItemFromPool(explosion, pos, Quaternion.identity);

        if (score < 1)
        {
            score = 1;
        }
        explosion.transform.localScale = new Vector3(1 + score * DataManager.Instance.bodySizeBuff, 1 + score * DataManager.Instance.bodySizeBuff, 1);
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

        Debug.Log("score = " + score);
        Debug.Log("numberOfBalls = " + numberOfBalls);
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
