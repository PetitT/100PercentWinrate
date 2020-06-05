using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerManager : MonoBehaviourPun
{
    [SerializeField] private DuelPlayerHealth health;
    [SerializeField] private DuelPlayerInput input;
    [SerializeField] private BoxCollider2D collision;
    [SerializeField] private GameObject avatar;
    [SerializeField] private GameObject body;
    public int playerID { get; private set; }
    private int avatarID;

    public event Action onReset;

    public void SetID(int ID)
    {
        playerID = ID;
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            body.layer = 0;
        }
        avatarID = avatar.GetComponent<PhotonView>().ViewID;
        DuelGameManager.Instance.onNewRoundStart += OnNewRoundHandler;
        DuelGameManager.Instance.onRoundPause += OnRoundPauseHandler;
        health.onDeath += OnDeathHandler;
    }

    private void OnDestroy()
    {
        DuelGameManager.Instance.onNewRoundStart -= OnNewRoundHandler;
        DuelGameManager.Instance.onRoundPause -= OnRoundPauseHandler;
        health.onDeath -= OnDeathHandler;
    }

    private void OnRoundPauseHandler()
    {
        collision.enabled = false;
    }

    private void OnDeathHandler()
    {
        photonView.RPC("ToggleAvatar", RpcTarget.All, false, avatarID);
        if (photonView.IsMine)
        {
            byte lostRoundEvent = DuelDataManager.Instance.onLostRoundEvent;
            object[] eventObject = new object[] { photonView.ViewID };
            RaiseEventOptions options = new RaiseEventOptions() { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(lostRoundEvent, eventObject, options, SendOptions.SendReliable);
        }
    }

    private void OnNewRoundHandler()
    {
        Invoke("Reset", 0.25f);
    }

    private void Reset()
    {
        onReset?.Invoke();
        photonView.RPC("ToggleAvatar", RpcTarget.All, true, avatarID);
        collision.enabled = true;
    }
    
    [PunRPC]
    private void ToggleAvatar(bool toggle, int avatarID)
    {
        PhotonView.Find(avatarID).gameObject.SetActive(toggle);
    }
}
