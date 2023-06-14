using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    // Start is called before the first frame update

    private int score;
    
    private int high;

    private string Key = "highScore";

    private void Start()
    {
        ClearScore();
        Initialize();
    }

    private void Update()
    {
        if(gameManager.GameManager.isGameClear)
        {
            string str = gameManager.GameManager.GetScore().ToString();
            score = int.Parse(str);
            ClearScore();
            Save();
        }
    }

    public void ClearScore() 
    {
        if (score < 0)
        {
            score = 0;
        }

        if (this.gameObject.activeSelf)
        {
            if (high < score)
            {
                high = score;
            }

            scoreText.text = score.ToString();
            highScoreText.text = high.ToString();
            Debug.Log("ClearScore’Ê‰ß");
        }
    }

    private void Initialize()
    {
        high = PlayerPrefs.GetInt(Key,0);
        Debug.Log("InitScore’Ê‰ß");
    }

    public void Save()
    {
        PlayerPrefs.SetInt(Key,high);
        PlayerPrefs.Save();

    }



}
