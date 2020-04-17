using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    public class Vector1RangePropertyInfo : PropertyInfo
    {
        [Range(0, 1)] public float value;
        private Dictionary<MaterialInfo, float> defaultValues = new Dictionary<MaterialInfo, float>();

        public override void SaveDefaultValues()
        {
            defaultValues.Clear();
            foreach (var matInfo in affectedMatInfos)
            {
                defaultValues.Add(matInfo, matInfo.material.GetFloat(propertyName));
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
            PropertyModifyer.ModifyMaterial(value, propertyName, matInfo.material);
        }
    }
}
