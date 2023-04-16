using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Core core;

    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    private string animBoolName;

    public PlayerState(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoCheck();
        player.Anim.SetBool(animBoolName, true);
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
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
