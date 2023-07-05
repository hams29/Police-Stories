using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseFriendAction : PlayerState
{
    public PlayerUseFriendAction(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName):base(player,stateMachine,playerData,animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        player.inventoryUI.ShowInventoryUI();
        Movement?.SetVelocityZero();
        Rotation.CanSetRotate = false;
        Movement.CanSetVelocity = false;
    }

    public override void Exit()
    {
        base.Exit();

        Rotation.CanSetRotate = true;
        Movement.CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //InventoryInput‚ªfalse‚É‚È‚Á‚½‚Æ‚«
        if (!friendActionInput)
        {
            //player.inventoryUI.HideInventoryUI();

            if (player.inventoryUI.isSelect)
            {
            }

            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private bool CheckHaveGadget(int gadgetNumber)
    {
        bool re = false;
        int gadCount = player.Inventory.gadgets.Count;

        if (gadgetNumber <= gadCount)
            re = true;


        return re;
    }
}
