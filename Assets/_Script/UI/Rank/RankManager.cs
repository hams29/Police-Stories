using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{

    [SerializeField] private Text rankText;
    [SerializeField] private Text highRankText;

    enum tag_Rank
    {
        A,
        B, 
        C,
        D,
        E
    }


    private string Key = "highScore";

    private string rankStr = "";
    private string highRankStr = "";

    private int rank = (int)tag_Rank.E;

    private void Start()
    {
        //ClearRank();
        Initialize();
        Debug.Log("Start�ʉ�");
    }

    private void Update()
    {
        if (gameManager.GameManager.isGameClear)
        {
            gameManager.GameManager.Ranking();
            rankStr = gameManager.GameManager.GetRank();
            ClearRank();
            Save();
        }
    }

    public void ClearRank()
    {
        if (this.gameObject.activeSelf)
        {
            if (rank <= (int)tag_Rank.A)
            {
                
            }
            else if (rank < (int)tag_Rank.B)
            {

                
            }
            else if (rank > (int)tag_Rank.C)
            {


            }
            else if (rank > (int)tag_Rank.D)
            {


            }
            else if (rank > (int)tag_Rank.E)
            {


            }
            switch (rank)
            {
                case (int)tag_Rank.A:

                    highRankStr = "A";

                    break;
                case (int)tag_Rank.B:

                    highRankStr = "B";

                    break;
                case (int)tag_Rank.C:

                    highRankStr = "C";

                    break;
                case (int)tag_Rank.D:

                    highRankStr = "D";

                    break;
                case (int)tag_Rank.E:

                    highRankStr = "E";

                    break;
            }


            rankText.text = rankStr;
            highRankText.text = highRankStr;
            Debug.Log("ClearRank�ʉ�");
        }
    }

    private void Initialize()
    {
        highRankStr = PlayerPrefs.GetString(Key, "-");
        Debug.Log("InitRank�ʉ�");
    }

    public void Save()
    {
        PlayerPrefs.SetString(Key, highRankStr);
        PlayerPrefs.Save();

        Debug.Log("Save�ʉ�");
    }
}
