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

    [Header("GunType")]
    public GunType gunType;

    [Header("Probability correction"), Tooltip("èeÇégÇ¡ÇΩç€Ç…ìGÇ…ãCÇ√Ç©ÇÍÇÈämó¶ï‚ê≥Åi0Å`1Ç‹Ç≈Åj")]
    public float probCollect = 1.0f;

    public enum GunType
    {
        HandGun,
        AssaultRifle,
    }
}
