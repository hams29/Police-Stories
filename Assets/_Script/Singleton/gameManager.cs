using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    private GameObject weapon;

    [SerializeField] private Text text;

    private void Awake()
    {
        weapon = weaponSelect.weaponData;

        text.text = weapon.name;
    }



}
