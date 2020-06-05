using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkDataBinder : MonoBehaviour
{
    public string webAdress;
    public MonoBehaviour dataHolder;

    private IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(webAdress);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string[] splitData = www.downloadHandler.text.Split(';');
            BindData(splitData);
        }
    }

    private void BindData(string[] dataString)
    {
        FieldInfo[] fieldInfos;
        Type dataHolderType = dataHolder.GetType();
        fieldInfos = dataHolderType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

        for (int i = 0; i < dataString.Length - 1; i++)
        {
            string fieldName = GetDataName(dataString[i]);
            string valueType = GetValueType(dataString[i]);
            string value = dataString[i].Substring(dataString[i].IndexOf("Value:") + 6);
            object fieldValue = null;

            switch (valueType)
            {
                case "I":
                    fieldValue = Convert.ToInt32(value);
                    break;

                case "F":
                    fieldValue = float.Parse(value, CultureInfo.InvariantCulture);
                    break;

                case "B":
                    fieldValue = value == "1";
                    break;

                case "S":
                    fieldValue = value;
                    break;

                default:
                    Debug.LogError("Incorrect data type");
                    break;
            }
            FieldInfo fieldToChange = fieldInfos.Where(field => field.Name == fieldName).FirstOrDefault();
            fieldToChange.SetValue(dataHolder, fieldValue);
        }
    }

    private string GetDataName(string data)
    {
        string value = data.Substring(data.IndexOf("Name:") + 5);
        value = value.Remove(value.IndexOf('|'));
        return value;
    }

    private string GetValueType(string data)
    {
        string value = data.ElementAt(data.IndexOf("|") + 1).ToString();
        return value;
    }
}
