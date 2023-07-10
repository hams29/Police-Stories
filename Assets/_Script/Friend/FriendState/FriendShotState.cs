using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendShotState : FriendState
{
    public FriendShotState(FriendController friend, FriendStateMachine stateMachine, FriendData friendData, string animBoolName) : base(friend, stateMachine, friendData, animBoolName)
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

        Gun gun = friend.Inventory.mainWeapon.GetComponent<Gun>();

        if (gun != null)
        {
            gun.Shot();
            if(gun.GetCurrentMagazineAmmo() <= 0)
            {
                //リロードステータスに変更
                stateMachine.ChangeState(friend.ReloadState);
            }
        }
        else
        {
            Debug.LogError("Not Set Gun!!");
            stateMachine.ChangeState(friend.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
