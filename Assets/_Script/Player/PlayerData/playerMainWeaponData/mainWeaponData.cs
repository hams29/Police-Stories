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

    [Header("Weapon Shot Wait Time")]
    public float shotWaitTime = 0;

    [Header("FullAuto")]
    public bool fullAuto = false;

    [Header("Ammo Speed")]
    public float ammoSpeed = 20.0f;

    [Header("Damage")]
    public float shotDamage = 20.0f;
}
