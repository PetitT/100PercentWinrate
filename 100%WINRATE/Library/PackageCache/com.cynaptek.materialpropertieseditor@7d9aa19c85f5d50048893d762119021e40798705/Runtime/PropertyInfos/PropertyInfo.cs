using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    public abstract class PropertyInfo : MonoBehaviour
    {
        protected List<MaterialInfo> affectedMatInfos = new List<MaterialInfo>();
        public string propertyName;

        private void Awake()
        {
            GetMaterialInfos();
            SaveDefaultValues();
        }

        private void Update()
        {
            AffectMaterials();
        }

        private void OnDestroy()
        {
            ResetToDefault();
        }

        public void AffectMaterials()
        {
            foreach (var matInfo in affectedMatInfos)
            {
                UpdateMaterials(matInfo);
            }
        }

        public void GetMaterialInfos()
        {
            affectedMatInfos.Clear();
            foreach (var matinfo in GetComponentInParent<MaterialFinder>().materialInfos)
            {
                if (matinfo.affectThisMaterial)
                {
                    if (matinfo.material.HasProperty(propertyName))
                    {
                        affectedMatInfos.Add(matinfo);
                    }
                }
            }
        }

        public abstract void UpdateMaterials(MaterialInfo matInfo);

        public abstract void SaveDefaultValues();

        public abstract void ResetToDefault();
    }
}
