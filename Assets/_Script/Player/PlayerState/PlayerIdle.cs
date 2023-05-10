using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName) : base(player,stateMachine,playerData,animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isExitingState)
        {
            //TODO::PlayerIdle::各ステータスへ移行
            if (shotInput && player.gun.GetCurrentMagazineAmmo() > 0)
                stateMachine.ChangeState(player.ShotState);
            else if (reloadInput)
                stateMachine.ChangeState(player.ReloadState);
            else if (interactInput)
            {
                player.inputController.UseInteractInput();
                stateMachine.ChangeState(player.InteractState);
            }
            else if(callInput)
            {
                player.inputController.UseCallInput();
                stateMachine.ChangeState(player.CallState);
            }
            else if (xInput != 0 || zInput != 0)
            {
                if (dashinput)
                    stateMachine.ChangeState(player.RunState);
                else
                    stateMachine.ChangeState(player.MoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
