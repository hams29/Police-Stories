using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : PlayerState
{
    public PlayerMelee(PlayerController player,PlayerStateMachine stateMachine, PlayerData playerData,string animBoolName):base(player,stateMachine,playerData,animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = false;
        Movement?.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();

        player.inputController.UseMeleeInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        //�ߐڍU�����[�V�����I�������̃X�e�[�^�X�Ɉڍs
        if(isAnimationFinished)
        {
            if (xInput != 0 || zInput != 0)
            {
                if (dashinput)
                    stateMachine.ChangeState(player.RunState);
                else
                    stateMachine.ChangeState(player.MoveState);
            }
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
