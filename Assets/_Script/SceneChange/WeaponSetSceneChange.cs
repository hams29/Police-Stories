using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSetSceneChange : SceneChange
{
    [SerializeField]
    WeaponSet weaponSet;

    private void Awake()
    {
        nextSceneName = SelectStageName.stageName;
    }

    public void ChangeClick()
    {
        base.ChangeNextScene();
        if (gameManager.GameManager != null)
            gameManager.GameManager.setGun = weaponSet.setGun;
    }
}
