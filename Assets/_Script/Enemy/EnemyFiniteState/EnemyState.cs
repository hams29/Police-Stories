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
    protected States States { get => states ?? core.GetCoreComponent(ref states); }
    protected Interact Interact { get => interact ?? core.GetCoreComponent(ref interact); }
    protected Damage Damage { get => damage ?? core.GetCoreComponent(ref damage); }

    private Damage damage;
    private Interact interact;
    private Movement movement;
    private Rotation rotation;

    private States states;

    protected float startTIme;
    protected bool isAnimationFinishTrigger;

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
        isAnimationFinishTrigger = false;
    }

    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        if (States.dead && stateMachine.CurrentState != enemy.DeadState)
        {
            stateMachine.ChangeState(enemy.DeadState);
            stateMachine.SetCanChangeState(false);
        }
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void DoCheck()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimationFinishTrigger = true;

    protected void OpenFrontDoor()
    {
        //�h�A���J���鏈���i�܂��Ă��鎞�͉������Ȃ��j
        RaycastHit hitObject;
        Vector3 pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1.5f, enemy.transform.position.z);
        if (Physics.Raycast(pos, enemy.transform.forward, out hitObject, enemyData.interactDistance))
        {
            int layerNo = LayerMask.NameToLayer(enemyData.interactLayerName);
            if (hitObject.transform.gameObject.layer == layerNo)
            {
                if (DoorCheck(hitObject.transform.gameObject))
                {
                    Core otherCore = hitObject.transform.GetComponentInChildren<Core>();
                    if (otherCore != null)
                    {
                        Interact otherInteract = null;
                        otherCore.GetCoreComponent(ref otherInteract);
                        if (otherInteract != null)
                        {
                            if (otherInteract.canInteract)
                                otherInteract.SetInteract();
                        }
                    }
                }
            }
        }
    }

    private bool DoorCheck(GameObject obj)
    {
        bool ret = false;
        DoorOpen doorOpen = obj.GetComponent<DoorOpen>();
        if (doorOpen != null)
        {
            if (!doorOpen.isOpened)
                ret = true;
        }

        return ret;
    }
}
