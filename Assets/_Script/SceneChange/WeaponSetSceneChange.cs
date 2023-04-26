using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSetSceneChange : SceneChange
{

    private void Awake()
    {
        nextSceneName = SelectStageName.stageName;
    }

    public void ChangeClick()
    {
        base.ChangeNextScene();
    }
}
