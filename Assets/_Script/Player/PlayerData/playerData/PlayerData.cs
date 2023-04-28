using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player HP")]
    public float maxHP = 100.0f;

    [Header("Move State")]
    public float moveSpeed = 5.0f;
    public float runSpeed = 8.0f;

    [Header("Reload Move State")]
    public float reloadMoveSpeed = 1.5f;

    [Header("Melee")]
    public float meleeDamage = 50.0f;
    public float meleeDistance = 5.0f;
}
