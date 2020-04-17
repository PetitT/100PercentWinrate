using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    public class Vector4PropertyInfo : PropertyInfo
    {
        public Vector4 value;
        private Dictionary<MaterialInfo, Vector4> defaultValues = new Dictionary<MaterialInfo, Vector4>();

        public override void SaveDefaultValues()
        {
            defaultValues.Clear();
            foreach (var matInfo in affectedMatInfos)
            {
                defaultValues.Add(matInfo, matInfo.material.GetVector(propertyName));
            }
        }

        public override void ResetToDefault()
        {
            foreach (var matInfoValue in defaultValues)
            {
                PropertyModifyer.ModifyMaterial(matInfoValue.Value, propertyName, matInfoValue.Key.material);
            }
        }

        public override void UpdateMaterials(MaterialInfo matInfos)
        {
            PropertyModifyer.ModifyMaterial(value, propertyName, matInfos.material);
        }
    }
}
