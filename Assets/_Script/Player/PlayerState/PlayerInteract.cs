using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : PlayerState
{
    public PlayerInteract(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName):base(player,stateMachine,playerData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        RaycastHit hitObject;
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
        if (Physics.Raycast(pos,player.transform.forward,out hitObject,playerData.interactDistance))
        {
            int layerNo = LayerMask.NameToLayer(playerData.interactLayerName);
            if (hitObject.transform.gameObject.layer == layerNo)
            {
                Core otherCore = hitObject.transform.GetComponentInChildren<Core>();
                if (otherCore != null)
                {
                    Interact otherInteract = null;
                    otherCore.GetCoreComponent(ref otherInteract);
                    if(otherInteract != null)
                    {
                        if(otherInteract.canInteract)
                            otherInteract.SetInteract();
                    }
                }
            }
            else
                Debug.Log(hitObject.transform.name + "��interactLayer�ł͂���܂���B Layer : " + hitObject.transform.gameObject.layer.ToString());
        }

        stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
