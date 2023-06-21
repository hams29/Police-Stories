using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Detection : EnemyState
{
    private Enemy1ScoreData scoreData;
    public Enemy1Detection(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName,Enemy1ScoreData scoreData):base(enemy,stateMachine,enemyData,animBoolName)
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
        Interact.canInteract = false;
        if (gameManager.GameManager != null)
        {
            gameManager.GameManager.AddScore(scoreData.enemyAddDeadScore);
            gameManager.GameManager.addEliminatedEnemy();
        }
        Debug.Log(enemy.name + " ÇçSë©");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Damage.isDamage)
        {
            if (gameManager.GameManager != null)
                gameManager.GameManager.AddScore(scoreData.enemySubDetectionShotScore);
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
