using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1PlayerSearch : EnemyState
{
    private Gun gun;
    public Enemy1PlayerSearch(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
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

        //プレイヤーの方向に向く
        Rotation.SetRotation(enemy.PlayerSearch.playerPos);
        if (gun.GetCurrentMagazineAmmo() <= 0)
        {
            stateMachine.ChangeState(enemy.ReloadState);
        }
        else if (Time.time > enemyData.playerSearchTime + startTIme && !enemy.PlayerSearch.isPlayerDead)
        {
            stateMachine.ChangeState(enemy.ShotState);
        }

        //プレイヤーを見失った際にIdleステータスに移行
        if (!enemy.PlayerSearch.isPlayerFind)
            stateMachine.ChangeState(enemy.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
