using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioHighPassFilter highPassFilter;
    [SerializeField] private int maxHighPassValue;
    [SerializeField] private float blendSpeed;

    public AudioSource AudioSource { get => audioSource; private set => audioSource = value; }
    public AudioHighPassFilter HighPassFilter { get => highPassFilter; private set => highPassFilter = value; }

    public void BlendInHighPass()
    {
        StartCoroutine(BlendIn());
    }

    public void BlendOutHighPass()
    {
        StartCoroutine(BlendOut());
    }

    private IEnumerator BlendIn()
    {
        while (HighPassFilter.cutoffFrequency > 10)
        {
            HighPassFilter.cutoffFrequency -= blendSpeed * Time.deltaTime;
            yield return null;
        }
        HighPassFilter.cutoffFrequency = 10;
    }

    private IEnumerator BlendOut()
    {
        while (HighPassFilter.cutoffFrequency < maxHighPassValue)
        {
            HighPassFilter.cutoffFrequency += blendSpeed * Time.deltaTime;
            yield return null;
        }
        HighPassFilter.cutoffFrequency = maxHighPassValue;
    }
}
