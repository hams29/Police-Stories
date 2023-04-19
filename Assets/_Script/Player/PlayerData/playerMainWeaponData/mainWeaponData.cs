using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMainWeaponData", menuName = "Data/Weapon Data/Base Data")]
public class mainWeaponData : ScriptableObject
{
    [Header("Weapon Name")]
    public string weaponName = "";

    [Header("Weapon Magazine Size")]
    public int maxAmmo = 0;

    [Header("Weapon Shoot Reaction")]
    public float shotReaction = 0;

    [Header("Weapon Responce")]
    public float shotResponce = 0;
}
