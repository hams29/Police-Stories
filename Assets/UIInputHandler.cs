using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    private void Update()
    {

        
        if (gameManager.GameManager.isPlayerDead)
        {
            if (Input.GetAxis("Fire2") > 0.1f)
            {
                gameManager.GameManager?.ReloadNowScene();
            }
        }


        if (gameManager.GameManager.isGameClear)
        {
            if (Input.GetAxis("Fire2") > 0.1f)
            {
                gameManager.GameManager?.ReloadNowScene();
            }
            else if (Input.GetAxisRaw("Submit") > 0.1f)
            {
                gameManager.GameManager?.StartStageSelectScene();
            }
        }
    }
}
