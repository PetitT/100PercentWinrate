using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MaterialProperties
{
    [CustomEditor(typeof(MaterialFinder))]
    public class MaterialFinderEditor : Editor
    {
        private MaterialFinder materialFinder;

        public override void OnInspectorGUI()
        {
            materialFinder = (MaterialFinder)target;
            base.OnInspectorGUI();

            if (GUILayout.Button(new GUIContent("Find Materials", "Find all materials in scene")))
            {
                materialFinder.GetMaterials();
            }

            if (GUILayout.Button(new GUIContent("Find Affected GameObjects", "Find all the GameObjects with that material")))
            {
                materialFinder.CheckAffectedItems();
            }

        }
    }
}