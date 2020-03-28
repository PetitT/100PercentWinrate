using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pool : MonoBehaviour
{
    public static Pool instance;

    public List<GameObject> objectsToPool;
    public int numberToCreateAtStart;

    private Dictionary<GameObject, List<GameObject>> pool = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        SetAsSingleton();

        foreach (var item in objectsToPool)
        {
            AddToPool(item);
        }
    }

    private void SetAsSingleton()
    {
        if (instance)
            Destroy(this);
        else
            instance = this;
    }

    private void AddToPool(GameObject item)
    {
        pool.Add(item, new List<GameObject>());
        for (int i = 0; i < numberToCreateAtStart; i++)
        {
            GameObject newItem = Instantiate(item);
            newItem.SetActive(false);
            pool[item].Add(newItem);
        }
    }

    public GameObject GetItemFromPool(GameObject item, Vector3 position)
    {
        if (!pool.ContainsKey(item))
        {
            AddToPool(item);
        }

        GameObject newItem = pool[item].Where(x => !x.activeSelf).FirstOrDefault();

        if (!newItem)
        {
            newItem = Instantiate(item);
            pool[item].Add(newItem);
        }
        newItem.transform.position = position;
        newItem.SetActive(true);

        return newItem;
    }
}
