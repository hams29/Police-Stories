using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public static string stageName;
    [SerializeField] protected string nextSceneName;

    protected void ChangeNextScene()
    {
        stageName = nextSceneName;
        SceneManager.LoadScene(stageName);
    }
}
