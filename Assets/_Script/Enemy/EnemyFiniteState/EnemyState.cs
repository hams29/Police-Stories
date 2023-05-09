using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy1Controller enemy;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;

    protected Core core;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected Rotation Rotation { get => rotation ?? core.GetCoreComponent(ref rotation); }

    private Movement movement;
    private Rotation rotation;

    protected States States { get => states ?? core.GetCoreComponent(ref states); }
    private States states;

    protected float startTIme;

    protected Vector3 workspace;
    private string animBoolName;

    public EnemyState(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        this.animBoolName = animBoolName;
        core = this.enemy.Core;
    }

    public virtual void Enter()
    {
        DoCheck();
        enemy.Anim.SetBool(animBoolName, true);
        startTIme = Time.time;
    }

    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        if (States.dead)
            stateMachine.ChangeState(enemy.DeadState);
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void DoCheck()
    {

    }
}
