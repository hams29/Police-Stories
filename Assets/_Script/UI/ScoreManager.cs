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
        string str = gameManager.GameManager.GetScore().ToString();
        score = int.Parse(str);
        ClearScore();
        InitScore();
        Debug.Log("Start’Ê‰ß");
    }

    private void Update()
    {
        if(gameManager.GameManager.isGameClear)
        {
            ClearScore();
            Save();
        }
    }

    public void ClearScore() 
    {
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

    private void InitScore()
    {
        high = PlayerPrefs.GetInt(Key,0);
        Debug.Log("InitScore’Ê‰ß");
    }

    public void Save()
    {
        PlayerPrefs.SetInt(Key,high);
        PlayerPrefs.Save();

        Debug.Log("Save’Ê‰ß");
    }

}
