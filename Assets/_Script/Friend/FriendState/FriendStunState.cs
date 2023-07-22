using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendStunState : FriendState
{
    public FriendStunState(FriendController friend, FriendStateMachine stateMachine,FriendData friendData,string animBoolName):base(friend,stateMachine,friendData,animBoolName)
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
     
        if(States?.nowWeakening == States.WeakeningState.None)
        {
            stateMachine.ChangeState(stateMachine.OldState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
