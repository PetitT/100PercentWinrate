using MaterialProperties;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorGradientAnimator : MonoBehaviour
{
    [SerializeField] private ColorGradientPropertyInfo propertyInfo;

    private float blendTime;

    private void Start()
    {
        blendTime = DataManager.Instance.lineColorBlendTime;
        propertyInfo.gradient = GetNewGradient(GetNewColor(), GetNewColor());
        StartCoroutine(UpdateColors());
    }

    private IEnumerator UpdateColors()
    {
        while (propertyInfo.blend < 1)
        {
            propertyInfo.blend += 1 / blendTime * Time.deltaTime;
            yield return null;
        }
        propertyInfo.gradient = GetNewGradient(propertyInfo.gradient.Evaluate(1), GetNewColor());
        propertyInfo.blend = 0;
        StartCoroutine(UpdateColors());
    }

    private Gradient GetNewGradient(Color baseColor, Color targetColor)
    {
        Gradient newGradient = new Gradient();
        GradientColorKey[] colors = { new GradientColorKey(baseColor, 0), new GradientColorKey(targetColor, 1) };
        GradientAlphaKey[] alphas = { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1) };
        newGradient.SetKeys(colors, alphas);
        return newGradient;
    }

    private Color GetNewColor()
    {
        Color newColor = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        return newColor;
    }
}
