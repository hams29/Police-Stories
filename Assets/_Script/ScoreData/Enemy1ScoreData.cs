using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyScoreData", menuName = "Data/Enemy Data/Score Data")]
public class Enemy1ScoreData : ScriptableObject
{
    [Header("Enemy Add Score")]
    public float enemyAddDeadScore = 50.0f;
    public float enemyAddDetactionScore = 100.0f;
    public float enemyAddShotScore = 10.0f;
    public float enemyAddSurrender = 50.0f;

    [Header("Enemy Sub Score")]
    public float enemySubDeadScore = -50.0f;
    public float enemySubDetectionShotScore = -100.0f;
    public float enemySubShotScore = -10.0f;
    public float enemySubSurrenderShotScore = -80.0f;
}
