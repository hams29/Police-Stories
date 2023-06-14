using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RunTimeManager : MonoBehaviour
{
    [SerializeField] private Text runTimeMinutesText;
    [SerializeField] private Text runTimeText;

    [SerializeField] private Text highRunTimeMinutesText;
    [SerializeField] private Text highRunTimeText;

    private float clearTime;
    private float minutes;

    private float minClearTime;
    private float minMinutes;

    private string Key = "highTime";
    private string Key1 = "highMinutes";

    private void Start()
    {
        ClearRunTime();
        Initialize();
    }

    private void Update()
    {
        if (gameManager.GameManager.isGameClear)
        {
            clearTime = gameManager.GameManager.GetTime();
            minutes = gameManager.GameManager.GetMinutes();

            ClearRunTime();
            Save();
        }
    }

    public void ClearRunTime()
    {
        if (this.gameObject.activeSelf)
        {

            if (minutes < minMinutes)
            {
                minMinutes = minutes;
            }

            runTimeMinutesText.text = minutes.ToString();
            runTimeText.text = clearTime.ToString();

            if (clearTime < minClearTime && minutes < minMinutes)
            {
                minClearTime = clearTime;
            }

            highRunTimeMinutesText.text = minMinutes.ToString();
            highRunTimeText.text = minClearTime.ToString();

        }
    }

    public void Initialize()
    {
        minClearTime = PlayerPrefs.GetFloat(Key, 0.0f);
        minMinutes = PlayerPrefs.GetFloat(Key1, 0.0f);        
    }

    public void Save()
    {

        PlayerPrefs.SetFloat(Key, minClearTime);
        PlayerPrefs.SetFloat(Key1, minMinutes);
        PlayerPrefs.Save();

    }

}
