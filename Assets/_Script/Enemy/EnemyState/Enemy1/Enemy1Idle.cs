using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Idle : EnemyState
{
    public Enemy1Idle(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName):base(enemy,stateMachine,enemyData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(enemy.PlayerSearch.isPlayerFind)
        {
            stateMachine.ChangeState(enemy.PlayerSearchState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
