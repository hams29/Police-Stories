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

    protected void OpenFrontDoor()
    {
        //ドアを開ける処理（閉まっている時は何もしない）
        RaycastHit hitObject;
        Vector3 pos = new Vector3(friend.transform.position.x, friend.transform.position.y + 1.5f, friend.transform.position.z);
        if (Physics.Raycast(pos, friend.transform.forward, out hitObject, friendData.interactDistance))
        {
            int layerNo = LayerMask.NameToLayer(friendData.interactLayerName);
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
