using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PostProcessController
{
    public class ColorAdjustmentController : PostProcessController<ColorAdjustments>
    {
        public float postExposure;
        [Range(-100, 100)] public float contrast;
        [ColorUsage(false, true)] public Color colorFilter;
        [Range(-180, 180)] public float hueShift;
        [Range(-100, 100)] public float saturation;

        protected override void UpdateComponent()
        {
            if (Component.postExposure.overrideState)
            {
                Component.postExposure.value = postExposure;
            }
            if (Component.contrast.overrideState)
            {
                Component.contrast.value = contrast;
            }
            if (Component.colorFilter.overrideState)
            {
                Component.colorFilter.value = colorFilter;
            }
            if (Component.hueShift.overrideState)
            {
                Component.hueShift.value = hueShift;
            }
            if (Component.saturation.overrideState)
            {
                Component.saturation.value = saturation;
            }
        }

        protected override void SetBaseValues()
        {
            postExposure = Component.postExposure.value;
            contrast = Component.contrast.value;
            colorFilter = Component.colorFilter.value;
            hueShift = Component.hueShift.value;
            saturation = Component.saturation.value;
        }
    }
}
