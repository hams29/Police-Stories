using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject mainWeapon { get; private set; }
    public mainWeaponData.GunType gunType { get; private set; }
    [SerializeField]
    private bool isPlayer;

    //TODO::Inventory::��ŏ���
    [SerializeField]
    private GameObject debugMainWeapon;

    private void Awake()
    {
        if(!isPlayer)
        {
            if (debugMainWeapon != null)
                mainWeapon = debugMainWeapon;

            if (mainWeapon != null)
                gunType = mainWeapon.GetComponent<Gun>().GetMainWeaponData().gunType;
        }
    }
    private void Start()
    {
    }

    public void SetMainWeapon()
    {
        if(gameManager.GameManager != null)
        {
            if(gameManager.GameManager.setGun != null)
                mainWeapon = gameManager.GameManager.setGun.gun;
            else if (debugMainWeapon != null)
                mainWeapon = debugMainWeapon;
        }
        else if (debugMainWeapon != null)
            mainWeapon = debugMainWeapon;

        if(mainWeapon != null)
            gunType = mainWeapon.GetComponent<Gun>().GetMainWeaponData().gunType;
    }

    /*
    public void SetMainWeapon(GameObject mainWeapon)
    {
        this.mainWeapon = mainWeapon;
    }
    */
}
