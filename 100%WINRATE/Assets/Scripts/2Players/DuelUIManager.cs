using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuelUIManager : PunSingleton<DuelUIManager>
{
    [SerializeField] private List<TextMeshProUGUI> playerNames;
    [SerializeField] private List<GameObject> playerLives;

    private List<GameObject> hearts = new List<GameObject>();

    private Dictionary<int, TextMeshProUGUI> nameDisplay = new Dictionary<int, TextMeshProUGUI>();
    private Dictionary<int, GameObject> livesDisplay = new Dictionary<int, GameObject>();

    private void Start()
    {
        hearts.Add(DuelDataManager.Instance.redHeart);
        hearts.Add(DuelDataManager.Instance.blueHeart);
    }

    public void AddPlayer(int playerNumber, int ID, string name, int lives)
    {
        photonView.RPC("AddNewPlayer", RpcTarget.AllBuffered,playerNumber, ID, name, lives);
    }

    public void UpdateLives(int ID, int lives)
    {
        UpdatePlayerLives(ID, lives);
    }

    [PunRPC]
    private void AddNewPlayer(int playerNumber, int ID, string name, int lives)
    {
        nameDisplay.Add(ID, playerNames[playerNumber]);
        livesDisplay.Add(ID, playerLives[playerNumber]);

        nameDisplay[ID].text = name;

        for (int i = 0; i < lives; i++)
        {
            GameObject newLife = Pool.instance.GetItemFromPool(hearts[playerNumber], Vector3.zero, Quaternion.identity);
            newLife.transform.SetParent(livesDisplay[ID].gameObject.transform);
        }
    }

    private void UpdatePlayerLives(int ID, int lives)
    {
        int lifes = livesDisplay[ID].transform.childCount -1;
        GameObject lastHeart = livesDisplay[ID].transform.GetChild(lifes).gameObject;
        lastHeart.transform.parent = null;
        lastHeart.SetActive(false);
    }
}
