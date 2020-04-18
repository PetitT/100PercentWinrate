using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PostProcessController
{
    public class ChromaticAberrationController : PostProcessController<ChromaticAberration>
    {
        public float intensity;

        protected override void SetBaseValues()
        {
            intensity = Component.intensity.value;
        }

        protected override void UpdateComponent()
        {
            if (Component.intensity.overrideState)
            {
                if (intensity > Component.intensity.max)
                {
                    Component.intensity.max = intensity;
                }
                Component.intensity.value = intensity;
            }
        }
    }
}
