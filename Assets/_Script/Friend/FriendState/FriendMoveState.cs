using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMoveState : FriendState
{
    private Vector3 targetPosition;

    public FriendMoveState(FriendController friend,FriendStateMachine stateMachine,FriendData friendData,string animBoolName):base(friend,stateMachine,friendData,animBoolName)
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
        friend.navAgent.SetDestination(targetPosition);

        //追従しなくなった場合その場で待機する
        if (!friend.isFollow)
            stateMachine.ChangeState(friend.IdleState);

        Vector2 ppos = new Vector2(gameManager.GameManager.player.transform.position.x, gameManager.GameManager.player.transform.position.z);
        Vector2 fpos = new Vector2(friend.transform.position.x, friend.transform.position.z);
        float distance = Vector2.Distance(ppos, fpos);
        SetPosition(gameManager.GameManager.player.transform.position);
        OpenFrontDoor();

        if (friend.search.isDetected)
        {
            stateMachine.ChangeState(friend.DetectedState);
        }
        else if (distance < friendData.playerAround)
        {
            //プレイヤーに近づいたら待機状態に戻る
            stateMachine.ChangeState(friend.IdleState);
        }
        else if (States?.nowWeakening == States.WeakeningState.FrashBang)
        {
            stateMachine.ChangeState(friend.StunState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetPosition(Vector3 pos)
    {
        targetPosition = pos;
    }
}
