using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : PlayerState
{
    public PlayerRun(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData, string animBoolName):base(player,stateMachine,playerData,animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        canMelee = false;
    }

    public override void Exit()
    {
        base.Exit();

        canMelee = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (inventoryInput)
        {
            stateMachine.ChangeState(player.UseInventoryState);
        }
        else if (reloadInput)
            stateMachine.ChangeState(player.ReloadState);
        else if (xInput == 0 && zInput == 0)
            stateMachine.ChangeState(player.IdleState);
        else if (!dashinput)
            stateMachine.ChangeState(player.MoveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        workspace = new Vector3(xInput, 0, zInput);
        Movement?.SetVelocity(workspace, playerData.runSpeed);
        Vector3 pos = player.transform.position;
        workspace = new Vector3(workspace.x + pos.x, workspace.y + pos.y, workspace.z + pos.z);
        Rotation.SetRotation(workspace);
    }
}
