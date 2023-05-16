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

    [Header("Player SearchTime")]
    public float playerSearchTime = 1.5f;

    [Header("Enemy Shot Count")]
    public int enemyShotCount = 5;

    [Header("Enemy Surrender Probability")]
    public float surrenderProbability = 50.0f;

    [Header("Enemy Detantion Time")]
    public float detantionTime = 2.0f;

    [Header("Enemy Player Out of View Time")]
    public float playerOutOfViewTime = 1.5f;
}
