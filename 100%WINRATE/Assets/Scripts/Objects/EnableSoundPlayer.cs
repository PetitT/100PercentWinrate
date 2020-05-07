using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private void OnEnable()
    {
        source.PlayOneShot(source.clip);
    }
}
