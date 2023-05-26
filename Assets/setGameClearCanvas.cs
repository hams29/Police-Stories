using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setGameClearCanvas : MonoBehaviour
{
    private void Start()
    {
        gameManager.GameManager?.setGameClearCanvas(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
