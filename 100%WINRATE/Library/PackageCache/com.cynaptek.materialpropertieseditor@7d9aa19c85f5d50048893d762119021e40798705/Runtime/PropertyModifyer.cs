using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MaterialProperties
{
    public static class PropertyModifyer
    {
        public static void ModifyMaterial(float value, string propertyName, Material affectedMaterial)
        {
            affectedMaterial.SetFloat(propertyName, value);
        }

        public static void ModifyMaterial(Color value, string propertyName, Material affectedMaterial)
        {
            affectedMaterial.SetColor(propertyName, value);
        }

        public static void ModifyMaterial(bool value, string propertyName, Material affectedMaterial)
        {
            affectedMaterial.SetInt(propertyName, value ? 1 : 0);
        }

        private static void ModifyVector(Vector4 value, string propertyName, Material affectedMaterial)
        {
            affectedMaterial.SetVector(propertyName, value);
        }

        public static void ModifyMaterial(Texture2D value, string propertyName, Material affectedMaterial)
        {
            affectedMaterial.SetTexture(propertyName, value);
        }

        public static void ModifyMaterial(Color defaultColor, Color targetColor, float blendValue, string propertyName, Material affectedMaterial)
        {
            Color newColor = GetBlendededColor(defaultColor, targetColor, blendValue, propertyName);
            affectedMaterial.SetColor(propertyName, newColor);
        }

        private static Color GetBlendededColor(Color defaultColor, Color targetColor, float blendValue, string propertyName)
        {
            Gradient gradient = new Gradient();
            GradientColorKey firstKey = new GradientColorKey(defaultColor, 0);
            GradientColorKey lastKey = new GradientColorKey(targetColor, 1);
            GradientAlphaKey firstAlpha = new GradientAlphaKey(defaultColor.a, 0);
            GradientAlphaKey lastAlpha = new GradientAlphaKey(targetColor.a, 1);
            gradient.SetKeys(new GradientColorKey[]
            {
            firstKey, lastKey
            },
            new GradientAlphaKey[]
            {
            firstAlpha, lastAlpha
            });
            Color newColor = gradient.Evaluate(blendValue);
            return newColor;
        }

        public static void ModifyMaterial(Vector2 value, string propertyName, Material affectedMaterial)
        {
            ModifyVector(value, propertyName, affectedMaterial);
        }

        public static void ModifyMaterial(Vector3 value, string propertyName, Material affectedMaterial)
        {
            ModifyVector(value, propertyName, affectedMaterial);
        }

        public static void ModifyMaterial(Vector4 value, string propertyName, Material affectedMaterial)
        {
            ModifyVector(value, propertyName, affectedMaterial);
        }
    }
}
