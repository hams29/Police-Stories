using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] protected string nextSceneName;


    public void ChangeNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
