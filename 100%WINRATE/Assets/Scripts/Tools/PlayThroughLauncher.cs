using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayThroughLauncher : MonoBehaviour
{
    private void Update()
    {
#if(UNITY_EDITOR)
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("LogScene"))
            {
                SceneManager.LoadScene("LogScene");
                EditorApplication.EnterPlaymode();
            }
        }
#endif
    }
}
