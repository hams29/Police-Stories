using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject mainWeapon { get; private set; }
    public mainWeaponData.GunType gunType { get; private set; }

    //TODO::Inventory::å„Ç≈è¡Ç∑
    [SerializeField]
    private GameObject debugMainWeapon;

    private void Awake()
    {
        if (debugMainWeapon != null)
            mainWeapon = debugMainWeapon;

        if(mainWeapon != null)
            gunType = mainWeapon.GetComponent<Gun>().GetMainWeaponData().gunType;
    }
    private void Start()
    {
    }

    public void SetMainWeapon(GameObject mainWeapon)
    {
        this.mainWeapon = mainWeapon;
    }
}
