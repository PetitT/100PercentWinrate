using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip sound;

    private void OnEnable()
    {
        source.PlayOneShot(sound);
    }
}
