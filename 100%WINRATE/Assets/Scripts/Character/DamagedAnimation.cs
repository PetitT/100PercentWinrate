using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DamagedAnimation : MonoBehaviour
{
    [SerializeField] private PlayerCollision collision;
    private PostProcessVolume ppv;
    private ChromaticAberration CA;

    float value = 0;
    private float maxValue;
    private float animSpeed;
    private float animSpeedBack;

    private void Start()
    {
        animSpeedBack = DataManager.Instance.damagedAnimationSpeedRecover;
        animSpeed = DataManager.Instance.damagedAnimationSpeed;
        maxValue = DataManager.Instance.chromaticAberrationMaxValue;
        collision.onHit += OnHitHandler;
        ppv = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        ppv.profile.TryGetSettings(out CA);
    }

    private void OnDisable()
    {
        collision.onHit -= OnHitHandler;
    }

    private void OnHitHandler(float damage)
    {
        StopCoroutine(AnimateHit());
        StartCoroutine(AnimateHit());
    }

    private IEnumerator AnimateHit()
    {
        while (value < maxValue)
        {
            value += Time.deltaTime * animSpeed;
            CA.intensity.value = value;
            yield return null;
        }
        while (value > 0)
        {
            value -= Time.deltaTime * animSpeedBack;
            CA.intensity.value = value;
            yield return null;
        }
    }
}
