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

    [Header("Player SearchTime") ,Tooltip("敵がプレイヤーを見つけてから撃つまでの時間")]
    public float playerSearchTime = 1.5f;

    [Header("Enemy Shot Count"), Tooltip("連射可能な武器を所持している場合に連続で射撃する回数")]
    public int enemyShotCount = 5;

    [Header("Enemy Surrender Probability"), Tooltip("降参する確率")]
    public float surrenderProbability = 50.0f;

    [Header("Enemy Detantion Time"),Tooltip("拘束する時間")]
    public float detantionTime = 2.0f;

    [Header("Enemy Player Out of View Time"),Tooltip("敵がプレイヤーを見失って巡回に戻るまでの時間")]
    public float playerOutOfViewTime = 1.5f;

    [Header("Enemy Interact Distance")]
    public float interactDistance = 2.5f;
    public string interactLayerName;

    [Header("Enemy Here Probability"), Tooltip("音で気づく確率（0～100まで）")]
    public float soundHereProbability = 40.0f;
}
