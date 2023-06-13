using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseInventory : PlayerState
{
    public PlayerUseInventory(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName):base(player,stateMachine,playerData,animBoolName)
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
        if(!inventoryInput)
        {
            player.inventoryUI.HideInventoryUI();

            if(player.inventoryUI.isSelect)
            {
                switch(player.inventoryUI.selectItem)
                {
                    case PlayerInventoryUI.Item.mainWeapon:
                        player.isHaveMainWeapon = true;
                        break;

                    case PlayerInventoryUI.Item.gadget1:
                        if (CheckHaveGadget((int)PlayerInventoryUI.Item.gadget1))
                        {
                            player.isHaveMainWeapon = false;
                            player.nowHaveGadget = 0;
                        }
                        else
                            player.isHaveMainWeapon = true;
                        break;

                    case PlayerInventoryUI.Item.gadget2:
                        if (CheckHaveGadget((int)PlayerInventoryUI.Item.gadget2))
                        {
                            player.isHaveMainWeapon = false;
                            player.nowHaveGadget = 1;
                        }
                        else
                            player.isHaveMainWeapon = true;
                        break;

                    case PlayerInventoryUI.Item.gadget3:
                        if (CheckHaveGadget((int)PlayerInventoryUI.Item.gadget3))
                        {
                            player.isHaveMainWeapon = false;
                            player.nowHaveGadget = 2;
                        }
                        else
                            player.isHaveMainWeapon = true;
                        break;
                }
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
