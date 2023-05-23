using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Death : EnemyState
{
    public Enemy1Death(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName):base(enemy,stateMachine,enemyData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        if (gameManager.GameManager != null)
        {
            gameManager.GameManager.addEliminatedEnemy();

            if(enemy.enemySurrenderProbability)
                gameManager.GameManager.AddScore(50.0f);
            else
                gameManager.GameManager.AddScore(-50.0f);
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
