using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject mainWeapon { get; private set; }
    private void Start()
    {

    }

    public void SetMainWeapon(GameObject mainWeapon)
    {
        this.mainWeapon = mainWeapon;
    }
}
