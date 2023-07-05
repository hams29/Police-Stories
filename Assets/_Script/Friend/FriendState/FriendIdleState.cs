using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendIdleState : FriendState
{
    public FriendIdleState(FriendController friend,FriendStateMachine stateMachine, FriendData friendData, string animBoolName) : base(friend,stateMachine,friendData,animBoolName)
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

        if(friend.isFollow)
        {
            //’Ç]’†‚Ì
            Vector2 ppos = new Vector2(gameManager.GameManager.player.transform.position.x, gameManager.GameManager.player.transform.position.z);
            Vector2 fpos = new Vector2(friend.transform.position.x, friend.transform.position.z);
            float distance = Vector2.Distance(ppos, fpos);
            if(distance > friendData.playerAround)
            {
                //ƒvƒŒƒCƒ„[‚ÉŒü‚©‚Á‚Ä“®‚­
                friend.MoveState.SetPosition(gameManager.GameManager.player.transform.position);
                stateMachine.ChangeState(friend.MoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
