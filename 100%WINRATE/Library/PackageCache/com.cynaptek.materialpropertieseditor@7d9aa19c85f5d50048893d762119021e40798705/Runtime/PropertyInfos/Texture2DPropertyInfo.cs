using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    public class Texture2DPropertyInfo : PropertyInfo
    {
        public Texture2D texture;
        private Dictionary<MaterialInfo, Texture2D> defaultValues = new Dictionary<MaterialInfo, Texture2D>();

        public override void SaveDefaultValues()
        {
            defaultValues.Clear();
            foreach (var matInfo in affectedMatInfos)
            {
                defaultValues.Add(matInfo, (Texture2D)matInfo.material.GetTexture(propertyName));
            }
        }

        public override void ResetToDefault()
        {
            foreach (var matInfoValue in defaultValues)
            {
                PropertyModifyer.ModifyMaterial(matInfoValue.Value, propertyName, matInfoValue.Key.material);
            }
        }

        public override void UpdateMaterials(MaterialInfo matinfo)
        {
            PropertyModifyer.ModifyMaterial(texture, propertyName, matinfo.material);
        }
    }
}
