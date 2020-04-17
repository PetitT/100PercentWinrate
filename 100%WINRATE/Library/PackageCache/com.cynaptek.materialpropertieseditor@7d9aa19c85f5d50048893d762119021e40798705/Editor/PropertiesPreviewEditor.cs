using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MaterialProperties
{
    [CustomEditor(typeof(PropertiesPreview))]
    public class PropertiesPreviewEditor : Editor
    {
        PropertiesPreview propertiesPreview;

        public override void OnInspectorGUI()
        {
            propertiesPreview = (PropertiesPreview)target;

            if (!propertiesPreview.isPreviewing)
            {
                if (GUILayout.Button("Start Preview"))
                {
                    propertiesPreview.StartPreview();
                }
            }
            else
            {
                if (GUILayout.Button("End Preview"))
                {
                    propertiesPreview.EndPreview();
                }
            }

            if (propertiesPreview.isPreviewing)
            {
                EditorGUILayout.HelpBox("You are currently previewing changes on Material Editor, you must end the preview before entering play mode", MessageType.Warning);
            }
        }
    }
}
