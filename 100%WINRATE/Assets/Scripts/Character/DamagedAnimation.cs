using PostProcessController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedAnimation : MonoBehaviour
{
    [SerializeField] private PlayerCollision collision;
    private ChromaticAberrationController cac;

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
        cac = FindObjectOfType<ChromaticAberrationController>();
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
            cac.intensity = value;
            yield return null;
        }
        while (value > 0)
        {
            value -= Time.deltaTime * animSpeedBack;
            cac.intensity = value;
            yield return null;
        }
    }
}
