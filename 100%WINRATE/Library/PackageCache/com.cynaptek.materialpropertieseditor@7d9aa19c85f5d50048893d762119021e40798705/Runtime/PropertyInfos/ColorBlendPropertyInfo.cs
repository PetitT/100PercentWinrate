using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    public class ColorBlendPropertyInfo : PropertyInfo
    {
        public Color targetColor;
        [Range(0, 1)] public float blendValue;

        private Dictionary<MaterialInfo, Color> defaultValues = new Dictionary<MaterialInfo, Color>();

        public override void SaveDefaultValues()
        {
            defaultValues.Clear();
            foreach (var matInfo in affectedMatInfos)
            {
                defaultValues.Add(matInfo, matInfo.material.GetColor(propertyName));
            }
        }

        public override void ResetToDefault()
        {
            foreach (var matInfoValue in defaultValues)
            {
                PropertyModifyer.ModifyMaterial(matInfoValue.Value, propertyName, matInfoValue.Key.material);
            }
        }     

        public override void UpdateMaterials(MaterialInfo matInfo)
        {
            try
            {
                PropertyModifyer.ModifyMaterial(defaultValues[matInfo], targetColor, blendValue, propertyName, matInfo.material);
            }
            catch
            {

            }
        }
    }
}
