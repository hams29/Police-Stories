using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenState
{
    protected CitizenController citizen;
    protected CitizenStateMachine stateMachine;
    protected CitizenData citizenData;

    protected Core core;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected Rotation Rotation { get => rotation ?? core.GetCoreComponent(ref rotation); }
    protected States States { get => states ?? core.GetCoreComponent(ref states); }
    protected Interact Interact { get => interact ?? core.GetCoreComponent(ref interact); }
    protected Damage Damage { get => damage ?? core.GetCoreComponent(ref damage); }

    private Damage damage;
    private Interact interact;
    private Movement movement;
    private Rotation rotation;
    private States states;

    protected float startTime;
    protected bool isAnimationFinishTrigger;
    protected Vector3 workspace;
    private string animBoolName;

    public CitizenState(CitizenController citizen,CitizenStateMachine stateMachine,CitizenData citizenData,string animBoolName)
    {
        this.citizen = citizen;
        this.stateMachine = stateMachine;
        this.citizenData = citizenData;
        this.animBoolName = animBoolName;
        core = this.citizen.Core;
    }

    public virtual void Enter()
    {
        DoCheck();
        citizen.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinishTrigger = false;
    }

    public virtual void Exit()
    {
        citizen.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void DoCheck()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimationFinishTrigger = true;
}
