using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PostProcessController
{
    public class BloomController : PostProcessController<Bloom>
    {
        public float thresold;
        public float intensity;
        [Range(0,1)] public float scatter;
        [ColorUsage(false)] public Color tint;
        public float clamp;
        public bool highQualityRendering;
        public Texture dirtTexture;
        public float dirtIntensity;

        protected override void SetBaseValues()
        {
            thresold = Component.threshold.value;
            intensity = Component.intensity.value;
            scatter = Component.scatter.value;
            tint = Component.tint.value;
            clamp = Component.clamp.value;
            highQualityRendering = Component.highQualityFiltering.value;
            dirtTexture = Component.dirtTexture.value;
            dirtIntensity = Component.dirtIntensity.value;
        }

        protected override void UpdateComponent()
        {
            if (Component.threshold.overrideState)
            {
                Component.threshold.value = thresold;
            }
            if (Component.intensity.overrideState)
            {
                Component.intensity.value = intensity;
            }
            if (Component.scatter.overrideState)
            {
                Component.scatter.value = scatter;
            }
            if (Component.tint.overrideState)
            {
                Component.tint.value = tint;
            }
            if (Component.clamp.overrideState)
            {
                Component.clamp.value = clamp;
            }
            if (Component.highQualityFiltering.overrideState)
            {
                Component.highQualityFiltering.value = highQualityRendering;
            }
            if (Component.dirtTexture.overrideState)
            {
                Component.dirtTexture.value = dirtTexture;
            }
            if (Component.dirtIntensity.overrideState)
            {
                Component.dirtIntensity.value = dirtIntensity;
            }
        }
    }
}
