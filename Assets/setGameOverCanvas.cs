using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setGameOverCanvas : MonoBehaviour
{
    private void Start()
    {
        gameManager.GameManager?.setGameOverCanvas(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
