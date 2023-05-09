using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Idle : EnemyState
{
    private Gun gun;
    private float lockTime = 0;

    public Enemy1Idle(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName):base(enemy,stateMachine,enemyData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        gun = enemy.mainWeapon.GetComponent<Gun>();
        Movement?.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time < lockTime)
            return;

        if (gun.GetCurrentMagazineAmmo() <= 0)
        {
            stateMachine.ChangeState(enemy.ReloadState);
        }
        else if (enemy.PlayerSearch.isPlayerFind)
        {
            stateMachine.ChangeState(enemy.PlayerSearchState);
        }
        else
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetLockTime(float time) { lockTime = time; }
}
