using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerState
{
    public PlayerMove(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName):base(player,stateMachine,playerData,animBoolName)
    {
    }

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

        GameObject other;

        //�ʂ̃X�e�[�^�X�Ɉڍs
        if (shotInput && player.gun.GetCurrentMagazineAmmo() <= 0)
            stateMachine.ChangeState(player.ShotState);
        else if (reloadInput)
            stateMachine.ChangeState(player.ReloadState);
        else if (interactInput && player.CheckFrontObject("target", out other))
        {
            Core otherCore = other.GetComponentInChildren<Core>();
            if (otherCore != null)
            {
                Interact otherInteract = null;
                otherCore.GetCoreComponent(ref otherInteract);
                if (otherInteract.canInteract)
                {
                    player.DetantionState.SetOtherInteractComponent(otherInteract);
                    player.inputController.UseInteractInput();
                    stateMachine.ChangeState(player.DetantionState);
                }
            }
        }
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
        else if (xInput == 0 && zInput == 0)
            stateMachine.ChangeState(player.IdleState);
        else if (dashinput)
            stateMachine.ChangeState(player.RunState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        workspace = new Vector3(xInput, 0, zInput);
        Movement?.SetVelocity(workspace, playerData.moveSpeed);
    }
}
