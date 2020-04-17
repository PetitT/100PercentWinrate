using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    public class Vector3PropertyInfo : PropertyInfo
    {
        public Vector3 value;
        private Dictionary<MaterialInfo, Vector3> defaultValues = new Dictionary<MaterialInfo, Vector3>();

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
