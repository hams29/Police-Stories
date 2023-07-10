using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendReloadState : FriendState
{
    public FriendReloadState(FriendController friend, FriendStateMachine stateMachine, FriendData friendData, string animBoolName) : base(friend, stateMachine, friendData, animBoolName)
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


        if(isAnimationFinished)
        {
            Gun gun = friend.Inventory.mainWeapon.GetComponent<Gun>();
            gun.MaxAmmo();
            stateMachine.ChangeState(friend.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
