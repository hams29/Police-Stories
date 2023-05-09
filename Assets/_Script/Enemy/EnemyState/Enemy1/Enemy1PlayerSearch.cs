using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1PlayerSearch : EnemyState
{
    public Enemy1PlayerSearch(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
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

        //�v���C���[�̕����Ɍ���
        Rotation.SetRotation(enemy.PlayerSearch.playerPos);
        if(Time.time > enemyData.playerSearchTime + startTIme)
        {
            stateMachine.ChangeState(enemy.ShotState);
        }

        //�v���C���[�����������ۂ�Idle�X�e�[�^�X�Ɉڍs
        if (!enemy.PlayerSearch.isPlayerFind)
            stateMachine.ChangeState(enemy.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}