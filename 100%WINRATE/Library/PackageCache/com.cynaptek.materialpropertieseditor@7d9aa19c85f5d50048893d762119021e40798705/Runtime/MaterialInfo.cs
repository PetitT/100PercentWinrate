using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    [System.Serializable]
    public class MaterialInfo
    {
        [HideInInspector] public string name;
        public Material material;
        public bool affectThisMaterial = true;
        public List<GameObject> affectedObjects = new List<GameObject>();

        public MaterialInfo(Material mat)
        {
            material = mat;
            Rename(false);
        }

        public void Rename(bool affected)
        {
            if (material)
            {
                name = material.name + " - " + (affectThisMaterial ? "[X]" : "[  ]") + (affected ? " [D]" : " [  ]");
            }
        }
    }
}