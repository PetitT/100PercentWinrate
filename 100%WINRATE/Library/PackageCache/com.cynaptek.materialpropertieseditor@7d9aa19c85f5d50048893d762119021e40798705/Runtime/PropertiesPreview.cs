using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    [ExecuteInEditMode]
    public class PropertiesPreview : MonoBehaviour
    {
        public bool isPreviewing = false;

        private List<PropertyInfo> properties = new List<PropertyInfo>();

        public void StartPreview()
        {
#if(UNITY_EDITOR)
            isPreviewing = true;
            FindPropertyInfos();
            InitializePropertyInfos();
            UpdateMaterials();
#endif
        }

        public void EndPreview()
        {
#if(UNITY_EDITOR)
            isPreviewing = false;
            ResetDefaultValues();
#endif
        }

        private void FindPropertyInfos()
        {
            properties.Clear();
            foreach (var propertyInfo in FindObjectsOfType<PropertyInfo>())
            {
                properties.Add(propertyInfo);
            }
        }

        private void InitializePropertyInfos()
        {
            foreach (var property in properties)
            {
                property.GetMaterialInfos();
                property.SaveDefaultValues();
            }
        }

        private void Update()
        {
#if(UNITY_EDITOR)
            if (isPreviewing)
            {
                if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    UnityEditor.EditorApplication.ExitPlaymode();
                    Debug.LogError("Can't enter play mode while Properties Preview is previewing");
                }
            }

            if (isPreviewing)
            {
                UpdateMaterials();
            }
#endif
        }

        private void UpdateMaterials()
        {
            foreach (var property in properties)
            {
                property.AffectMaterials();
            }
        }

        private void ResetDefaultValues()
        {
            foreach (var property in properties)
            {
                property.ResetToDefault();
            }
        }
    }

}
