using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSelect : MonoBehaviour
{
    public static GameObject weaponData;
    [SerializeField] private GameObject weaponPrefab;

    public void SetWeapon()
    {
        weaponData = weaponPrefab;
    }

}
