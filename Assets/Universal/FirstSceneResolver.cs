using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneResolver : MonoBehaviour
{
    void Start()
    {
#if UNITY_SERVER && !UNITY_EDITOR
SceneManager.LoadScene(1, LoadSceneMode.Additive);
#endif

#if (UNITY_STANDALONE || UNITY_STANDALONE_WIN) && !UNITY_EDITOR
SceneManager.LoadScene(1, LoadSceneMode.Additive);
SceneManager.LoadScene(2, LoadSceneMode.Additive);
SceneManager.LoadScene(5, LoadSceneMode.Additive);
#endif
    }

}
