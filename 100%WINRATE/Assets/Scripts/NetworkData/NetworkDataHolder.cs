using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(NetworkDataFetcher))]
public class NetworkDataHolder : MonoBehaviour
{
    NetworkDataFetcher dataFetcher;
    DataManager dataManager;

    private void Start()
    {
        dataManager = DataManager.Instance;
        dataFetcher = GetComponent<NetworkDataFetcher>();
        dataFetcher.onFetchedData += OnFetchedDataHandler;
    }

    private void OnDestroy()
    {
        dataFetcher.onFetchedData -= OnFetchedDataHandler;
    }

    private void OnFetchedDataHandler(string[] dataString)
    {
        FieldInfo[] fieldInfos;
        Type dataManagerType = typeof(DataManager);
        fieldInfos = dataManagerType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

        for (int i = 0; i < dataString.Length - 1; i++)
        {
            FieldInfo fieldToChange = fieldInfos.Where(field => field.Name == GetDataName(dataString[i])).FirstOrDefault();
            fieldToChange.SetValue(dataManager, GetDataValue(dataString[i]));
        }
    }

    private string GetDataName(string data)
    {
        string value = data.Substring(data.IndexOf("Name:") + 5);
        value = value.Remove(value.IndexOf('|'));
        return value;
    }

    private float GetDataValue(string data)
    {
        string value = data.Substring(data.IndexOf("Value:") + 6);
        float newValue = float.Parse(value, CultureInfo.InvariantCulture);
        return newValue;
    }
}

