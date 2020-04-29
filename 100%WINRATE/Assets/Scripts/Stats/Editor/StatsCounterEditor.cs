using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StatsCounter))]
public class StatsCounterEditor : Editor
{
    StatsCounter statsCounter;
    public override void OnInspectorGUI()
    {
        statsCounter = (StatsCounter)target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Count Stats"))
        {
            statsCounter.GetInfos();
        }
    }
}
