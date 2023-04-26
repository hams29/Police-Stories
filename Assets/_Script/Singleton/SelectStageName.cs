using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageName : MonoBehaviour
{

    public static string stageName;

    [SerializeField] private string[] nextStageName;

    private int sceneNo;


    public void SetStage()
    {
        
        sceneNo = SelectStageChange.sceneId;
        stageName = nextStageName[sceneNo];
    }

}
