using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager GameManager;

    private GameObject weapon;
    private string stageName;

    [SerializeField] private Text text;
    [SerializeField] private Text text2;

    private void Awake()
    {
        if (GameManager != null)
            GameManager = this;
        else
            Destroy(this);

        weapon = weaponSelect.weaponData;
        stageName = SelectStageName.stageName;

        text.text = (!weapon) ? "" : weapon.name;
        text2.text = stageName;
    }



}
