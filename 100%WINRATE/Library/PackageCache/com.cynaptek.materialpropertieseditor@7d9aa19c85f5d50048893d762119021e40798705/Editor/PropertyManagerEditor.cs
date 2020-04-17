using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MaterialProperties
{
    [CustomEditor(typeof(PropertyManager))]
    public class PropertyManagerEditor : Editor
    {
        PropertyManager propertyManager;

        string[] options = { "Vector1", "Vector1 Range", "Vector2", "Vector3", "Vector4", "Color", "Color Gradient", "Color Blend", "Boolean", "Texture2D" };
        int index;

        public override void OnInspectorGUI()
        {
            propertyManager = (PropertyManager)target;

            index = EditorGUILayout.Popup("Property to add", index, options);

            if (GUILayout.Button("Add Property"))
            {
                AddProperty();
            }
        }

        private void AddProperty()
        {
            switch (index)
            {
                case 0:
                    propertyManager.AddProperty<Vector1PropertyInfo>();
                    break;

                case 1:
                    propertyManager.AddProperty<Vector1RangePropertyInfo>();
                    break;

                case 2:
                    propertyManager.AddProperty<Vector2PropertyInfo>();
                    break;

                case 3:
                    propertyManager.AddProperty<Vector3PropertyInfo>();
                    break;

                case 4:
                    propertyManager.AddProperty<Vector4PropertyInfo>();
                    break;

                case 5:
                    propertyManager.AddProperty<ColorPropertyInfo>();
                    break;

                case 6:
                    propertyManager.AddProperty<ColorGradientPropertyInfo>();
                    break;

                case 7:
                    propertyManager.AddProperty<ColorBlendPropertyInfo>();
                    break;

                case 8:
                    propertyManager.AddProperty<BooleanPropertyInfo>();
                    break;

                case 9:
                    propertyManager.AddProperty<Texture2DPropertyInfo>();
                    break;
            }
        }
    }
}
