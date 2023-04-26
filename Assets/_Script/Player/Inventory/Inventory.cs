using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject mainWeapon { get; private set; }

    //TODO::Inventory::��ŏ���
    [SerializeField]
    private GameObject debugMainWeapon;

    private void Awake()
    {
        if (debugMainWeapon != null)
            mainWeapon = debugMainWeapon;        
    }
    private void Start()
    {
    }

    public void SetMainWeapon(GameObject mainWeapon)
    {
        this.mainWeapon = mainWeapon;
    }
}
