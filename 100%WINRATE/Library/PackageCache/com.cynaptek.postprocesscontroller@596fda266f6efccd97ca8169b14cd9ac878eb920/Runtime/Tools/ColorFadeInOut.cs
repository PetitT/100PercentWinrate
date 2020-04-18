using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PostProcessController
{
    namespace PostProcessController
    {
        public class ColorFadeInOut : MonoBehaviour
        {
            [SerializeField] private ColorAdjustmentController controller;

            [Header("Fade In")]
            public bool fadeInOnStart;
            [Tooltip("Time to wait before the start of the fade in, the color and contrast will be set immediately after the function is called")]
            public float yieldTimeBeforeFadeIn;
            public float fadeInSpeed;
            public Color fadeInColor;
            private Color colorAfterFade = Color.white;
            private float contrastAfterFade = 0;

            [Header("FadeOut")]
            public float fadeOutSpeed;
            public Color fadeOutColor;

            [Header("Callbacks")]
            public UnityEvent onFadeInCompleted;
            public UnityEvent onFadeOutCompleted;

            private void Start()
            {
                colorAfterFade = controller.colorFilter;
                contrastAfterFade = controller.contrast;

                if (fadeInOnStart)
                {
                    FadeIn();
                }
            }

            public void FadeIn()
            {
                StartCoroutine(FadeInRoutine());
            }

            public void FadeOut()
            {
                StartCoroutine(FadeOutRoutine());
            }

            private IEnumerator FadeInRoutine()
            {
                controller.Component.contrast.overrideState = true;
                controller.Component.colorFilter.overrideState = true;

                Gradient fadeInGradient = GetGradient(fadeInColor, colorAfterFade);
                float timeElapsed = 0;
                float remainingGradient = 0;
                float contrastPerSecond = (100 + contrastAfterFade) / fadeInSpeed;
                float gradientPerSecond = 1 / fadeInSpeed;

                controller.contrast = -100;
                controller.colorFilter = fadeInGradient.Evaluate(0);
                yield return new WaitForSeconds(yieldTimeBeforeFadeIn);


                while (timeElapsed < fadeInSpeed)
                {
                    controller.contrast += contrastPerSecond * Time.deltaTime;
                    remainingGradient += Time.deltaTime * gradientPerSecond;
                    controller.colorFilter = fadeInGradient.Evaluate(remainingGradient);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                controller.contrast = contrastAfterFade;
                controller.colorFilter = colorAfterFade;
                onFadeInCompleted?.Invoke();
            }

            private IEnumerator FadeOutRoutine()
            {
                controller.Component.contrast.overrideState = true;
                controller.Component.colorFilter.overrideState = true;

                Gradient gradient = GetGradient(controller.colorFilter, fadeOutColor);
                float timeElapsed = 0;
                float remainingGradient = 0;
                float contrastPerSecond = (100 + controller.contrast) / fadeOutSpeed;
                float gradientPerSecond = 1 / fadeOutSpeed;

                while (timeElapsed < fadeOutSpeed)
                {
                    controller.contrast -= contrastPerSecond * Time.deltaTime;
                    remainingGradient += Time.deltaTime * gradientPerSecond;
                    controller.colorFilter = gradient.Evaluate(remainingGradient);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                controller.contrast = -100;
                controller.colorFilter = gradient.Evaluate(1);
                onFadeOutCompleted?.Invoke();
            }

            private Gradient GetGradient(Color start, Color end)
            {
                Gradient newGradient = new Gradient();
                GradientColorKey one = new GradientColorKey(start, 0);
                GradientAlphaKey alphaOne = new GradientAlphaKey(255, 0);
                GradientColorKey two = new GradientColorKey(end, 1);
                GradientAlphaKey alphaTwo = new GradientAlphaKey(255, 1);
                GradientColorKey[] colors = new GradientColorKey[] { one, two };
                GradientAlphaKey[] alphas = new GradientAlphaKey[] { alphaOne, alphaTwo };

                newGradient.SetKeys(colors, alphas);

                return newGradient;
            }
        }
    }
}

