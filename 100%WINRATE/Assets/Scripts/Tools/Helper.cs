using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    /// <summary>
    /// Returns a random element from the given collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T GetRandomInCollection<T>(List<T> list)
    {
        int random = Random.Range(0, list.Count);
        T item = list[random];
        return item;
    }

    public static T GetRandomInCollection<T>(T[] array)
    {
        int random = Random.Range(0, array.Length);
        T item = array[random];
        return item;
    }

    public static bool GetRandomBool()
    {
        int random = Random.Range(0, 1);
        if(random <= 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
