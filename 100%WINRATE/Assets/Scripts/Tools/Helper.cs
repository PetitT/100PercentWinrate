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

    /// <summary>
    /// Returns a random boolean
    /// </summary>
    /// <returns></returns>
    public static bool GetRandomBool()
    {
        int random = Random.Range(0, 1);
        if (random <= 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Returns a 2-keys Gradient with the specified keys, float values must be between 0 and 1
    /// </summary>
    /// <param name="startColor">Color at the first key</param>
    /// <param name="endColor">Color at the last key</param>
    /// <param name="baseAlpha">Alpha at the first key</param>
    /// <param name="endAlpha">Alpha at the last key</param>
    /// <returns></returns>
    public static Gradient Get2KeysGradient(Color startColor, Color endColor, float baseAlpha, float endAlpha)
    {
        GradientAlphaKey alpha0 = new GradientAlphaKey(baseAlpha, 0);
        GradientAlphaKey alpha1 = new GradientAlphaKey(endAlpha, 1);
        GradientColorKey colorkey0 = new GradientColorKey(startColor, 0);
        GradientColorKey colorkey1 = new GradientColorKey(endColor, 1);
        GradientColorKey[] colorKeys = new GradientColorKey[] { colorkey0, colorkey1 };
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[] { alpha0, alpha1 };
        Gradient newGradient = new Gradient();
        newGradient.SetKeys(colorKeys, alphaKeys);
        return newGradient;
    }

    /// <summary>
    /// Returns a gradient with the specified keys. 
    /// </summary>
    /// <param name="timeColorPairs">Color value with it's specific time, float value must be between 0 and 1 </param>
    /// <param name="timeAlphaPairs">Alpha value with it's specific time, the second float value must be the time and must be between 0 and 1 </param>
    /// <returns></returns>
    public static Gradient GetMultiKeysGradient(Dictionary<float, Color> timeColorPairs, Dictionary<float, float> timeAlphaPairs)
    {
        List<GradientColorKey> colorKeys = new List<GradientColorKey>();
        List<GradientAlphaKey> alphakeys = new List<GradientAlphaKey>();
        foreach (var pair in timeColorPairs)
        {
            GradientColorKey newColorKey = new GradientColorKey(pair.Value, pair.Key);
            colorKeys.Add(newColorKey);
        }

        foreach (var item in timeAlphaPairs)
        {
            GradientAlphaKey newAlphaKey = new GradientAlphaKey(item.Value, item.Key);
            alphakeys.Add(newAlphaKey);
        }

        Gradient newGradient = new Gradient();
        newGradient.SetKeys(colorKeys.ToArray(), alphakeys.ToArray());
        return newGradient;
    }
}
