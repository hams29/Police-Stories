using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendOpenDoor : FriendState
{
    public FriendOpenDoor(FriendController friend, FriendStateMachine stateMachine, FriendData friendData, string animBoolName) : base(friend, stateMachine, friendData, animBoolName)
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
        if(friend.navAgent.enabled != false)
            friend.navAgent.SetDestination(friend.turgetPosition);

        Vector2 tpos = new Vector2(friend.turgetPosition.x, friend.turgetPosition.z);
        Vector2 fpos = new Vector2(friend.transform.position.x, friend.transform.position.z);
        float distance = Vector2.Distance(tpos, fpos);

        if (friend.search.isDetected)
        {
            stateMachine.ChangeState(friend.DetectedState);
        }
        else if (distance < 1.0f)
        {
            friend.navAgent.enabled = false;
            Vector3 rot = new Vector3(tpos.x, friend.transform.position.y, tpos.y);
            Rotation?.SetRotation(rot);
            if (isOpenFrontDoor())
            {
                //�h�A���J������ҋ@��Ԃɖ߂�
                stateMachine.ChangeState(friend.IdleState);
            }
        }        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
