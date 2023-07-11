using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public static string stageName;
    [SerializeField] protected string nextSceneName;
    [SerializeField] protected gameManager.Scene nextScene;

    protected void ChangeNextScene()
    {
        stageName = nextSceneName;
        SceneManager.LoadScene(stageName);
        if (gameManager.GameManager != null)
            gameManager.GameManager.SetNextScene(nextScene);
    }

    protected void SetScene(gameManager.Scene scene)
    {
        gameManager.GameManager.SetNextScene(scene);
    }
}
