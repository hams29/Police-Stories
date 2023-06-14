using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSetSceneChange : SceneChange
{
    [SerializeField]
    WeaponSet weaponSet;
    [SerializeField]
    private string debugStageName = "Stage1Scene";

    private void Awake()
    {
        nextSceneName = SelectStageName.stageName;
        if (nextSceneName == null) nextSceneName = debugStageName;
    }

    public void ChangeClick()
    {
        base.ChangeNextScene();
        if (gameManager.GameManager != null)
        {
            gameManager.GameManager.setGun = weaponSet.setGun;
            gameManager.GameManager.gadgetObjects = gadgetSelect.setGadgetTable;
        }
    }

    public WeaponSet GetWeaponset() { return weaponSet; }
}
