using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PostProcessController
{
    public class LiftGammaGainController : PostProcessController<LiftGammaGain>
    {
        [Header("Lift")]
        [ColorUsage(false, true)] public Color liftColor;
        [Range(-1, 1)] public float liftIntensity;

        [Header("Gamma")]
        [ColorUsage(false, true)] public Color gammaColor;
        [Range(-1, 1)] public float gammaIntensity;

        [Header("Gain")]
        [ColorUsage(false, true)] public Color gainColor;
        [Range(-1, 1)] public float gainIntensity;

        protected override void SetBaseValues()
        {
            liftColor.r = Component.lift.value.x;
            liftColor.g = Component.lift.value.y;
            liftColor.b = Component.lift.value.z;
            liftIntensity = Component.lift.value.w;
            gammaColor.r = Component.gamma.value.x;
            gammaColor.g = Component.gamma.value.y;
            gammaColor.b = Component.gamma.value.z;
            gammaIntensity = Component.gamma.value.w;
            gainColor.r = Component.gain.value.x;
            gainColor.g = Component.gain.value.y;
            gainColor.b = Component.gain.value.z;
            gainIntensity = Component.gain.value.w;
        }

        protected override void UpdateComponent()
        {
            if (Component.lift.overrideState)
            {
                Component.lift.value = new Vector4(liftColor.r, liftColor.g, liftColor.b, liftIntensity);
            }
            if (Component.gamma.overrideState)
            {
                Component.gamma.value = new Vector4(gammaColor.r, gammaColor.g, gammaColor.b, gammaIntensity); ;
            }
            if (Component.gain.overrideState)
            {
                Component.gain.value = new Vector4(gainColor.r, gainColor.g, gainColor.b, gainIntensity);
            }
        }
    }
}