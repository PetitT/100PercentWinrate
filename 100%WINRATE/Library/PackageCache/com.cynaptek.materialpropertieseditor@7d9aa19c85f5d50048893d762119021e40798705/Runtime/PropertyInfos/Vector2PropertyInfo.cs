using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    public class Vector2PropertyInfo : PropertyInfo
    {
        public Vector2 value;
        private Dictionary<MaterialInfo, Vector2> defaultValues = new Dictionary<MaterialInfo, Vector2>();

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

        public override void UpdateMaterials(MaterialInfo matInfo)
        {
            PropertyModifyer.ModifyMaterial(value, propertyName, matInfo.material);
        }
    }
}
