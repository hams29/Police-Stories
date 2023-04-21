using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Core core;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    protected Rotation Rotation { get => rotation ?? core.GetCoreComponent(ref rotation); }
    private Rotation rotation;

    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    protected int xInput;
    protected int zInput;

    protected bool dashinput;
    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected bool canMelee;
    protected bool canShot;

    protected Vector3 workspace;

    private string animBoolName;

    private bool meleeInput;

    public PlayerState(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = player.Core;
    }

    public virtual void Enter()
    {
        DoCheck();
        player.Anim.SetBool(animBoolName, true);
        isExitingState = false;
        canMelee = true;
        canShot = true;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        xInput = player.inputController.NormInputX;
        zInput = player.inputController.NormInputZ;
        dashinput = player.inputController.DashInput;

        meleeInput = player.inputController.MeleeInput;
        //�ߐڍU���Ɉڍs
        if (meleeInput && canMelee)
            stateMachine.ChangeState(player.MeleeState);
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void DoCheck() { }
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

    public bool GetCanShot() { return canShot; }
}
