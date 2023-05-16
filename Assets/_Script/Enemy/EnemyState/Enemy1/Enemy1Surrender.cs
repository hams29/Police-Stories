using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Surrender : EnemyState
{
    private bool isDetantion;
    private float detantionStartTime;
    public Enemy1Surrender(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName):base(enemy,stateMachine,enemyData,animBoolName)
    {
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

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
                //S‘©ó‘Ô‚ÉˆÚs
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
