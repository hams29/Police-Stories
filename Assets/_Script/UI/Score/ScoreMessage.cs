using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMessage : MonoBehaviour
{
    //TODO: TextAnimationÇégÇ¡ÇƒÇ›ÇÈÅB

    [SerializeField] private Text[] ScorePM = new Text[3];
    [SerializeField] private Text[] ScoreMsg = new Text[3];


    public static ScoreMessage scoreMessage { get; private set; }

    private void Awake()
    {
        if (!scoreMessage)
            scoreMessage = this;
        else
            Destroy(scoreMessage);
    }

    private void Start()
    {
        ScoreTextReset();
    }

    private void Update()
    {
        if (!gameManager.GameManager.isGameClear || !gameManager.GameManager.isPlayerDead)
        {
            TextInMsg();
        }
    }

    public void TextInMsg()
    {

        for (int i = 0; i < 3; i++)
        {

            string pm = gameManager.GameManager.GetScorePM();
            string msg = gameManager.GameManager.GetScoreMsg();

            if (msg != ScoreMsg[i + 2].text)
            {

                bool b = pm == "+";

                ScorePM[i + 2].color = (b) ? Color.blue : Color.red;
                ScoreMsg[i + 2].color = (b) ? Color.blue : Color.red;

                Color cPM = ScorePM[i + 2].color;
                Color cMsg = ScoreMsg[i + 2].color;

                

                ScorePM[i].text = ScorePM[i + 1].text;
                ScoreMsg[i].text = ScoreMsg[i + 1].text;
                string tmpPM = ScorePM[i + 2].text;
                string tmpMsg = ScoreMsg[i + 2].text;
                ScorePM[i + 2].text = pm;
                ScoreMsg[i + 2].text = msg;
                ScorePM[i + 1].text = tmpPM;
                ScoreMsg[i + 1].text = tmpMsg;

                pm = ScorePM[i + 1].text;
                if (b)
                {
                    ScorePM[i].color = Color.blue;
                    ScoreMsg[i].color = Color.blue;
                }

                pm = ScorePM[i + 2].text;
                if (b)
                {
                    ScorePM[i + 1].color = Color.blue;
                    ScoreMsg[i + 1].color = Color.blue;
                }


            }           

            break;
        }
    }

    public void ScoreTextReset()
    {
        for (int i = 0; i < 3; i++)
        {
            ScorePM[i].text = "";
            ScoreMsg[i].text = "";
        }
    }

}
