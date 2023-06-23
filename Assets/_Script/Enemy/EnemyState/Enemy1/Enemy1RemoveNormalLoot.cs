using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1RemoveNormalLoot : EnemyState
{

    private Enemy1ScoreData scoreData;
    public Enemy1RemoveNormalLoot(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy1ScoreData scoreData) : base(enemy, stateMachine, enemyData, animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
        enemy.navAgent.enabled = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Damage.isDamage)
        {
            if (enemy.enemySurrenderProbability)
            {
                if (gameManager.GameManager != null)
                    gameManager.GameManager.AddScore(scoreData.enemyAddShotScore);
            }
            else
            {
                if (gameManager.GameManager != null)
                    gameManager.GameManager.AddScore(scoreData.enemySubShotScore);
            }
        }

        workspace = enemy.MoveState.enemyLootList[enemy.MoveState.nowLootCount];
        enemy.navAgent.SetDestination(workspace);

        if (enemy.transform.position.x - workspace.x <= 0.1f && enemy.transform.position.x - workspace.x >= -0.1f)
        {
            if (enemy.transform.position.z - workspace.z <= 0.1f && enemy.transform.position.z - workspace.z >= -0.1f)
            {
                if (enemy.MoveState.nowLootCount + 1 >= enemy.MoveState.maxLootCount)
                {
                    enemy.MoveState.SetNowLootCount(0);
                    enemy.IdleState.SetLockTime(2.0f);
                    stateMachine.ChangeState(enemy.IdleState);
                }
                else
                {
                    enemy.MoveState.SetNowLootCount(enemy.MoveState.nowLootCount);
                    stateMachine.ChangeState(enemy.MoveState);
                }
            }
        }

        if (enemy.PlayerSearch.isPlayerFind)
            stateMachine.ChangeState(enemy.PlayerSearchState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
