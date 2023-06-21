using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Death : EnemyState
{
    private Enemy1ScoreData scoreData;
    public Enemy1Death(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName,Enemy1ScoreData scoreData):base(enemy,stateMachine,enemyData,animBoolName)
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

        if (gameManager.GameManager != null)
        {
            if(stateMachine.OldState != enemy.DetactionState)
                gameManager.GameManager.addEliminatedEnemy();

            if(enemy.enemySurrenderProbability && stateMachine.OldState != enemy.DetactionState)
                gameManager.GameManager.AddScore(scoreData.enemyAddDeadScore);
            else
                gameManager.GameManager.AddScore(scoreData.enemySubDeadScore);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
