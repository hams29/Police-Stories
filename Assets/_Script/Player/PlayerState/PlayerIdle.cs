using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

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

        GameObject other;

        if(player.CheckFrontObject("interact", playerData.interactDistance) && !isInteractUIShow)
        {
            isInteractUIShow = true;
            player.InteractUI.Show();
        }
        else if(!player.CheckFrontObject("interact", playerData.interactDistance) && isInteractUIShow)
        {
            isInteractUIShow = false;
            player.InteractUI.Hide();
        }

        if (!isExitingState)
        {
            //TODO::PlayerIdle::各ステータスへ移行
            if (shotInput && player.gun.GetCurrentMagazineAmmo() > 0)
            {
                stateMachine.ChangeState(player.ShotState);
            }
            else if (reloadInput)
                stateMachine.ChangeState(player.ReloadState);
            else if(interactInput && player.CheckFrontObject("target",out other, playerData.meleeDistance))
            {
                Core otherCore = other.GetComponentInChildren<Core>();
                if(otherCore != null)
                {
                    Interact otherInteract = null;
                    otherCore.GetCoreComponent(ref otherInteract);
                    if(otherInteract.canInteract)
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
