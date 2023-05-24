using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSelect : MonoBehaviour
{
    //public static GameObject weaponData;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private WeaponSet weaponSet;
    public void SetWeapon()
    {
        weaponSet.setGun.gun = weaponPrefab;
    }



}
