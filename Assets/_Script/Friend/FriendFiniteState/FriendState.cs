using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendState
{
    protected Core core;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    protected Rotation Rotation { get => rotation ?? core.GetCoreComponent(ref rotation); }
    private Rotation rotation;

    protected States States { get => states ?? core.GetCoreComponent(ref states); }
    private States states;

    protected FriendController friend;
    protected FriendStateMachine stateMachine;
    protected  FriendData friendData;

    protected float startTime;

    protected Vector3 workspace;

    protected bool isExitingState;
    protected bool isAnimationFinished;

    private string animBoolName;

    private bool meleeInput;

    public FriendState(FriendController friend, FriendStateMachine stateMachine, FriendData friendData, string animBoolName)
    {
        this.friend = friend;
        this.stateMachine = stateMachine;
        this.friendData = friendData;
        this.animBoolName = animBoolName;
        core = friend.Core;
    }

    public virtual void Enter()
    {
        DoCheck();
        friend.Anim.SetBool(animBoolName, true);
        isExitingState = false;
    }

    public virtual void Exit()
    {
        friend.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void DoCheck() { }
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
