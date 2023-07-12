using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSelect : MonoBehaviour
{
    //static public weaponSelect Instance;
    static public GunTable setGunTable = null;

    //public static GameObject weaponData;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private WeaponSet weaponSet;

    [SerializeField] private GunTable gunTable;

    private void Start()
    {
        /*
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        */
    }
    public void SetWeapon()
    {
        //weaponSet.setGun.gun = weaponPrefab;
        //weaponSet.setGun.gunTabele = this.gunTable;
        setGunTable = this.gunTable;
    }

    public GunTable GetWeapon()
    {
        return gunTable;
    }

}
