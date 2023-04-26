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

        //別のステータスに移行
        if (shotInput)
            stateMachine.ChangeState(player.ShotState);
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
