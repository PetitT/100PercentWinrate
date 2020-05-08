using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkDataFetcher : MonoBehaviour
{
    public event Action<string[]> onFetchedData;

    private IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/PetitT/100PercentWinrate/master/100%25WINRATE/Datas.txt");
        yield return www.SendWebRequest();
        
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string[] splitData = www.downloadHandler.text.Split(';');
            onFetchedData?.Invoke(splitData);
        }
    }
}
