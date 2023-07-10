using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FriendMovePointState : FriendState
{
    public FriendMovePointState(FriendController friend, FriendStateMachine stateMachine, FriendData friendData, string animBoolName) : base(friend, stateMachine, friendData, animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        friend.navAgent.enabled = true;
        friend.navAgent.speed = friendData.moveSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        friend.navAgent.enabled = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        friend.navAgent.SetDestination(friend.turgetPosition);

        Vector2 tpos = new Vector2(friend.turgetPosition.x, friend.turgetPosition.z);
        Vector2 fpos = new Vector2(friend.transform.position.x, friend.transform.position.z);
        float distance = Vector2.Distance(tpos, fpos);
        OpenFrontDoor();


        if (friend.search.isDetected)
        {
            stateMachine.ChangeState(friend.DetectedState);
        }
        else if (distance < friendData.playerAround)
        {
            //ポイントに近づいたら待機状態に戻る
            stateMachine.ChangeState(friend.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
