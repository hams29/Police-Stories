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

    [Header("Player SearchTime") ,Tooltip("�G���v���C���[�������Ă��猂�܂ł̎���")]
    public float playerSearchTime = 1.5f;

    [Header("Enemy Shot Count"), Tooltip("�A�ˉ\�ȕ�����������Ă���ꍇ�ɘA���Ŏˌ������")]
    public int enemyShotCount = 5;

    [Header("Enemy Surrender Probability"), Tooltip("�~�Q����m��")]
    public float surrenderProbability = 50.0f;

    [Header("Enemy Detantion Time"),Tooltip("�S�����鎞��")]
    public float detantionTime = 2.0f;

    [Header("Enemy Player Out of View Time"),Tooltip("�G���v���C���[���������ď���ɖ߂�܂ł̎���")]
    public float playerOutOfViewTime = 1.5f;

    [Header("Enemy Interact Distance")]
    public float interactDistance = 2.5f;
    public string interactLayerName;

    [Header("Enemy Here Probability"), Tooltip("���ŋC�Â��m���i0�`100�܂Łj")]
    public float soundHereProbability = 40.0f;
}
