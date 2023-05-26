using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text remainingAmmoText;
    [SerializeField] private Text maxAmmoText;

    [SerializeField] private Slider magazinSlider1;
    [SerializeField] private Slider magazinSlider2;
    [SerializeField] private Slider magazinSlider3;

    [SerializeField] private Image currentNPCStateImage;

    private void Start()
    {
        if (gameManager.GameManager != null)
        {
            gameManager.GameManager.SetGameUI(remainingAmmoText, scoreText, maxAmmoText, magazinSlider1, magazinSlider2, magazinSlider3, currentNPCStateImage);
            gameManager.GameManager.addCanvasObj(this.gameObject);
        }
    }
}
