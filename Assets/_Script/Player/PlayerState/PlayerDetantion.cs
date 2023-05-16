using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetantion : PlayerState
{
    private Interact otherInteract;
    public PlayerDetantion(PlayerController player, PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName):base(player,stateMachine,playerData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        if(otherInteract == null)
        {
            Debug.Log("PlayerDetantion : 敵のInteractコンポーネントがありません。");
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            otherInteract.SetInteract();
            Movement?.SetVelocityZero();
            Movement.CanSetVelocity = false;
            Rotation.CanSetRotate = false;
        }
    }

    public override void Exit()
    {
        Movement.CanSetVelocity = true;
        Rotation.CanSetRotate = true;
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //インタラクトボタンを離したときの処理
        if(player.inputController.MeleeInputStop)
        {
            otherInteract.UseInteract();
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetOtherInteractComponent(Interact interact) { otherInteract = interact; }
}
