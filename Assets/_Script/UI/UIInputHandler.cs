using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{

    private GameEndInputHandler inputController;

    private void Start()
    {
        if (InputManagerDontDestroy.Instance != null)
            inputController = InputManagerDontDestroy.Instance.gameEndInputHandler;
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
                gameManager.GameManager?.StartStageSelectScene();
                gameManager.GameManager?.ResetGameScene();
            }
        }
    }
}
