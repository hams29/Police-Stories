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

        //TODO::PlayerInteract::Interactèàóù

        RaycastHit hitObject;
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
        if (Physics.Raycast(pos,player.transform.forward,out hitObject,playerData.interactDistance))
        {
            int layerNo = LayerMask.NameToLayer(playerData.interactLayerName);
            if (hitObject.transform.root.gameObject.layer == layerNo)
            {
                Core otherCore = hitObject.transform.GetComponentInChildren<Core>();
                if (otherCore != null)
                {
                    Interact otherInteract = null;
                    otherCore.GetCoreComponent(ref otherInteract);
                    if(otherInteract != null)
                    {
                        otherInteract.SetInteract();
                    }
                }
            }
            else
                Debug.Log(hitObject.transform.root.name + "ÇÕinteractLayerÇ≈ÇÕÇ†ÇËÇ‹ÇπÇÒÅB Layer : " + hitObject.transform.root.gameObject.layer.ToString());
        }

        stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
