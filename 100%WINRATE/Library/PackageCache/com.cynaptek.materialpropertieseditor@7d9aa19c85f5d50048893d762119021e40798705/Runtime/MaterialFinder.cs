using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace MaterialProperties
{
    [RequireComponent(typeof(PropertyManager))]
    public class MaterialFinder : MonoBehaviour
    {
        public List<MaterialInfo> materialInfos = new List<MaterialInfo>();

        #region MonoBehaviour Callbacks

        private void OnValidate()
        {
            RenameMaterials();
        }

        #endregion

        #region Private Methods

        private void RenameMaterials()
        {
            foreach (MaterialInfo matInfo in materialInfos)
            {
                matInfo.Rename(FindIfAffected(matInfo));
            }
        }

        private bool FindIfAffected(MaterialInfo matInfo)
        {
            bool affected = false;

            foreach (MaterialFinder matFinderToCheck in FindObjectsOfType<MaterialFinder>())
            {
                if (matFinderToCheck != this)
                {
                    foreach (MaterialInfo otherMatInfo in matFinderToCheck.materialInfos)
                    {
                        Material matToCheck = otherMatInfo.material;
                        if (matInfo.material == matToCheck)
                        {
                            otherMatInfo.Rename(true);
                            affected = true;
                        }
                    }
                }
            }
            return affected;
        }

        #endregion
        #region Public Methods

        public void GetMaterials()
        {
            MeshRenderer[] meshesInScene = FindObjectsOfType<MeshRenderer>();

            foreach (var item in meshesInScene)
            {
                foreach (var material in item.sharedMaterials)
                {
                    if (!materialInfos.Where(mat => mat.material == material).Any())
                    {
                        materialInfos.Add(new MaterialInfo(material));
                    }
                }
            }
        }

        public void CheckAffectedItems()
        {
            foreach (MeshRenderer meshRenderer in FindObjectsOfType<MeshRenderer>())
            {
                foreach (Material mat in meshRenderer.sharedMaterials)
                {
                    foreach (MaterialInfo materialInfo in materialInfos)
                    {
                        if (mat == materialInfo.material)
                        {
                            if (!materialInfo.affectedObjects.Contains(meshRenderer.gameObject))
                            {
                                materialInfo.affectedObjects.Add(meshRenderer.gameObject);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
