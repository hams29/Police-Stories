using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseGadget : PlayerState
{
    GadgetBase useGadgets;
    public PlayerUseGadget(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName):base(player,stateMachine,playerData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityZero();
        Movement.CanSetVelocity = false;
        Rotation.CanSetRotate = false;

        useGadgets = player.Inventory.gadgets[player.nowHaveGadget];
        useGadgets.SetPlayerController(player,playerData);
        useGadgets.UseGadget();
    }

    public override void Exit()
    {
        base.Exit();
        Movement.CanSetVelocity = true;
        Rotation.CanSetRotate = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(shotInput && !useGadgets.isEnd)
        {
            useGadgets.LogicUpdate();
        }
        else
        {
            useGadgets.EndGadget();
            player.inputController.UseShotInput();
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
