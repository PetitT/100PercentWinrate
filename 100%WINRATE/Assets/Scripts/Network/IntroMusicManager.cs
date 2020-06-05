using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMusicManager : MonoBehaviour
{
    void Start()
    {
        MusicManager.Instance.BlendOutHighPass();
    }
}
