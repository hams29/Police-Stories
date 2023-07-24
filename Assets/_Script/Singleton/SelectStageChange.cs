using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageChange : MonoBehaviour
{

    static public int sceneId = -1;

    [SerializeField] private int selectStageId;
    [SerializeField] private SelectStageName selectStageName;

    public void SetStageId()
    {
        sceneId = selectStageId;
        selectStageName.SetStage();
    }

}
