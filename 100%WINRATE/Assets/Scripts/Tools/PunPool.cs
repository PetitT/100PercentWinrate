using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using System.Linq;

public class PunPool : PunSingleton<PunPool>
{
    [SerializeField] private List<string> objectsToPool;
    [SerializeField] private int numberToPoolAtStart;
    private List<GameObject> pool = new List<GameObject>();

    private void Start()
    {
        foreach (var item in objectsToPool)
        {
            for (int i = 0; i < numberToPoolAtStart; i++)
            {
                GameObject newItem = PhotonNetwork.Instantiate(Path.Combine(StringsManager.Instance.photon, item), Vector3.zero, Quaternion.identity);
                int itemID = newItem.GetComponent<PhotonView>().ViewID;
                photonView.RPC("ToggleItem", RpcTarget.AllBuffered, itemID, false);
                photonView.RPC("AddToList", RpcTarget.AllBuffered, itemID);
            }
        }
    }

    public GameObject GetItemFromPool(string prefabName, Vector3 position, Quaternion rotation)
    {
        GameObject newItem = null;
        foreach (GameObject item in pool)
        {
            if (item != null)
            {
                if (item.name.Contains(prefabName) && !item.activeSelf)
                {
                    newItem = item;
                    break;
                }
            }
        }
        if (!newItem)
        {
            newItem = PhotonNetwork.Instantiate(Path.Combine(StringsManager.Instance.photon, prefabName), Vector3.zero, Quaternion.identity);
            photonView.RPC("AddToList", RpcTarget.AllBuffered, newItem.GetComponent<PhotonView>().ViewID);
        }
        newItem.transform.position = position;
        newItem.transform.rotation = rotation;
        photonView.RPC("ToggleItem", RpcTarget.AllBuffered, newItem.GetComponent<PhotonView>().ViewID, true);
        return newItem;
    }

    [PunRPC]
    private void ToggleItem(int itemToHide, bool toggle)
    {
        PhotonView.Find(itemToHide).gameObject.SetActive(toggle);
    }

    [PunRPC]
    private void AddToList(int itemToAdd)
    {
        pool.Add(PhotonView.Find(itemToAdd).gameObject);
    }
}
