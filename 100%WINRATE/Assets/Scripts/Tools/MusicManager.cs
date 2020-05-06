using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioHighPassFilter highPassFilter;

    public AudioSource AudioSource { get => audioSource; private set => audioSource = value; }
    public AudioHighPassFilter HighPassFilter { get => highPassFilter; private set => highPassFilter = value; }
}
