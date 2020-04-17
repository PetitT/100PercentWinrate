using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    public class BooleanPropertyInfo : PropertyInfo
    {
        public bool value;
        private Dictionary<MaterialInfo, int> defaultValues = new Dictionary<MaterialInfo, int>();

        public override void SaveDefaultValues()
        {
            defaultValues.Clear();
            foreach (var matInfo in affectedMatInfos)
            {
                defaultValues.Add(matInfo, matInfo.material.GetInt(propertyName));
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
