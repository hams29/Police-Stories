using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="newEnemyData",menuName ="Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("EnemyHP")]
    public float maxHP = 100.0f;

    [Header("Move State")]
    public float moveSpeed = 5.0f;
    public float runSpeed = 8.0f;
}
