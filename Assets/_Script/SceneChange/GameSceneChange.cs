using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneChange : SceneChange
{

    private void Update()
    {
        //TODO : ここにゲームオーバーとゲームクリアの真偽値
        //
    }

    public void ChangeClick()
    {
        base.ChangeNextScene();
    }
}
