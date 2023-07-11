using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendDetected : FriendState
{
    public FriendDetected(FriendController friend, FriendStateMachine stateMachine, FriendData friendData, string animBoolName) : base(friend, stateMachine, friendData, animBoolName)
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

        if(friend.search.detectedEnemy != null)
            Rotation?.SetRotation(friend.search.detectedEnemy.transform.position);

        if(friend.search.detectedEnemy != null)
        {
            if(friend.search.detectedEnemy.enemySurrenderProbability && !friend.search.detectedEnemy.enemySurrender)
            {
                //射撃ステータスに移行
                Debug.Log("Friend is Shot!!");
                stateMachine.ChangeState(friend.ShotState);
            }
        }

        if (!friend.search.isDetected)
            stateMachine.ChangeState(friend.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
