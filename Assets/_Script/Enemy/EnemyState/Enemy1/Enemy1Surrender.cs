using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Surrender : EnemyState
{
    private bool isDetantion;
    private float detantionStartTime;
    private Enemy1ScoreData scoreData;
    public Enemy1Surrender(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName,Enemy1ScoreData scoreData):base(enemy,stateMachine,enemyData,animBoolName)
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
        Movement?.SetVelocityZero();
        Debug.Log(enemy.name + " is Surrender");
        isDetantion = false;
        detantionStartTime = 0.0f;
        Interact.canInteract = true;
        if (gameManager.GameManager != null)
            gameManager.GameManager.AddScore(scoreData.enemyAddSurrender);
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
                gameManager.GameManager.AddScore(scoreData.enemySubSurrenderShotScore);
        }    

        if(Interact.isInteract && !isDetantion)
        {
            isDetantion = true;
            detantionStartTime = Time.time;
        }
        else if(!Interact.isInteract && isDetantion)
        {
            isDetantion = false;
        }
        else if(isDetantion)
        {
            if(Time.time >= detantionStartTime + enemyData.detantionTime)
            {
                //�S����ԂɈڍs
                stateMachine.ChangeState(enemy.DetactionState);
            }
        }

        if(enemy.isPlayerOutOfView && Time.time >= enemy.playerOutOfViewTime + enemyData.playerOutOfViewTime)
        {
            Interact.canInteract = false;
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
