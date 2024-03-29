using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInputHandler : MonoBehaviour
{

    private GameEndInputHandler inputController { get => ic ?? InputManagerDontDestroy.Instance.GetGameEndUIInputHandler(ref ic);}
    private GameEndInputHandler ic;



    private void Start()
    {
    }
    private void Update()
    {
        if (inputController == null)
            return;

        if (gameManager.GameManager.isPlayerDead)
        {
            if (inputController.RestartInput)
            {
                inputController.UseRestartInput();
                gameManager.GameManager?.ReloadNowScene();
                gameManager.GameManager?.ResetGameScene();
            }
        }


        if (gameManager.GameManager.isGameClear)
        {
            if (inputController.RestartInput)
            {
                inputController.UseRestartInput();
                gameManager.GameManager?.ReloadNowScene();
                gameManager.GameManager?.ResetGameScene();
            }
            else if (inputController.TransSelectStageInput)
            {
                inputController.UseTransSelectStageInput();
                gameManager.GameManager?.StartStageWeaponSelectScene();
                gameManager.GameManager?.ResetGameScene();
                gameManager.GameManager?.SetNextScene(gameManager.Scene.GunSet);
            }
        }
    }

}
