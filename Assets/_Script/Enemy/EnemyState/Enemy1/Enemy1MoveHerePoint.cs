using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1MoveHerePoint : EnemyState
{
    private Enemy1ScoreData scoreData;
    public Enemy1MoveHerePoint(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy1ScoreData scoreData) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.scoreData = scoreData;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.navAgent.enabled = true;
        enemy.footImageManager.FootImageStart();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.navAgent.enabled = false;
        enemy.footImageManager.FootImageStop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.footImageManager.SetFootImagePosition(enemy.transform.position);

        if (Damage.isDamage)
        {
            if (enemy.enemySurrenderProbability)
            {
                if (gameManager.GameManager != null)
                {
                    gameManager.GameManager.AddScore(10.0f);
                    gameManager.GameManager.SetScorePM(true);
                    gameManager.GameManager.SetScoreMsg("敵にダメージ");
                    Debug.Log("Enemy1MovePoint");
                    ScoreMessage.scoreMessage.TextInMsg();
                }
            }
            else
            {
                if (gameManager.GameManager != null)
                {
                    gameManager.GameManager.AddScore(-10.0f);
                    gameManager.GameManager.SetScorePM(false);
                    gameManager.GameManager.SetScoreMsg("敵にダメージ");
                    Debug.Log("Enemy1MovePoint");
                    ScoreMessage.scoreMessage?.TextInMsg();
                }
            }
        }
        Vector3 lastPlayerPos = new Vector3(enemy.playerLastPos.x, 0, enemy.playerLastPos.z);
        enemy.navAgent.SetDestination(lastPlayerPos);

        if (lastPlayerPos.x + 0.5f > enemy.transform.position.x && lastPlayerPos.x - 0.5f < enemy.transform.position.x)
        {
            if (lastPlayerPos.z + 0.5f > enemy.transform.position.z && lastPlayerPos.z - 0.5f < enemy.transform.position.z)
            {
                enemy.IdleState.SetLockTime(2.0f);
                enemy.IdleState.SetNextState(enemy.RemoveNormalLootState);
                stateMachine.ChangeState(enemy.IdleState);
            }
        }

        //ドアを開ける処理（閉まっている時は何もしない）
        OpenFrontDoor();

        if (enemy.PlayerSearch.isSearchFind)
            stateMachine.ChangeState(enemy.PlayerSearchState);

        if (States?.nowWeakening == States.WeakeningState.FrashBang)
        {
            stateMachine.ChangeState(enemy.StunState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
