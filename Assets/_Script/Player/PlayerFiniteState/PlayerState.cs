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

    protected States States { get => states ?? core.GetCoreComponent(ref states); }
    private States states;

    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    protected int xInput;
    protected int zInput;

    protected bool dashinput;
    protected bool shotInput;
    protected bool reloadInput;
    protected bool interactInput;
    protected bool callInput;
    protected bool inventoryInput;
    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected bool canMelee;
    protected bool isInteractUIShow = false;

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
        shotInput = player.inputController.ShotInput;
        dashinput = player.inputController.DashInput;
        reloadInput = player.inputController.ReloadInput;
        interactInput = player.inputController.InteractInput;
        callInput = player.inputController.CallInput;
        inventoryInput = player.inputController.InventoryInput;

        meleeInput = player.inputController.MeleeInput;
        //‹ßÚUŒ‚‚ÉˆÚs
        if (meleeInput && canMelee)
            stateMachine.ChangeState(player.MeleeState);

        if(States.dead)
        {
            stateMachine.ChangeState(player.DeadState);
        }
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void DoCheck() { }
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
