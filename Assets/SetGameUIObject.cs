using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameUIObject : MonoBehaviour
{
    private void Start()
    {
        if(gameManager.GameManager != null)
        {
            gameManager.GameManager.addCanvasObj(this.gameObject);
        }
    }
}
