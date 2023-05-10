using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Surrender : EnemyState
{
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
