using PostProcessController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedAnimation : MonoBehaviour
{
    [SerializeField] private PlayerCollision collision;
    private ChromaticAberrationController cac;

    private float chromaticAberrationValue = 0;
    private float chromaticAberrationMaxValue;
    private float animSpeed;
    private float animSpeedBack;

    private float audioPitchValue = 1;
    private float audioPitchChangeSpeed;
    private float audioPitchMinValue;

    private void Start()
    {
        animSpeedBack = DataManager.Instance.damagedAnimationSpeedRecover;
        animSpeed = DataManager.Instance.damagedAnimationSpeed;
        chromaticAberrationMaxValue = DataManager.Instance.chromaticAberrationMaxValue;
        audioPitchChangeSpeed = DataManager.Instance.audioPitchChangeSpeed;
        audioPitchMinValue = DataManager.Instance.audioPitchMinValue;
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
        StopCoroutine(HitSound());
        StartCoroutine(HitSound());
    }

    private IEnumerator AnimateHit()
    {
        while (chromaticAberrationValue < chromaticAberrationMaxValue)
        {
            chromaticAberrationValue += Time.deltaTime * animSpeed;
            cac.intensity = chromaticAberrationValue;
            yield return null;
        }
        while (chromaticAberrationValue > 0)
        {
            chromaticAberrationValue -= Time.deltaTime * animSpeedBack;
            cac.intensity = chromaticAberrationValue;
            yield return null;
        }
    }

    private IEnumerator HitSound()
    {
        while(audioPitchValue > audioPitchMinValue)
        {
            audioPitchValue -= Time.deltaTime * audioPitchChangeSpeed;
            MusicManager.Instance.AudioSource.pitch = audioPitchValue;
            yield return null;
        }
        while(audioPitchValue < 1)
        {
            audioPitchValue += Time.deltaTime * audioPitchChangeSpeed;
            MusicManager.Instance.AudioSource.pitch = audioPitchValue;
            yield return null;
        }
        MusicManager.Instance.AudioSource.pitch = 1;
    }
}
