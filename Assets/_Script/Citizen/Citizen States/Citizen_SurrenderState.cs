using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen_SurrenderState : CitizenState
{
    private bool isDetaction;
    private float detactionStartTime;
    public Citizen_SurrenderState(CitizenController citizen,CitizenStateMachine stateMachine,CitizenData citizenData,string animBoolName):base(citizen,stateMachine,citizenData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();
        Debug.Log(citizen.name + " is Surrender");
        isDetaction = false;
        detactionStartTime = 0.0f;
        Interact.canInteract = true;
        
        gameManager.GameManager?.AddScore(50.0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Damage.isDamage)
            gameManager.GameManager?.AddScore(-80.0f);

        if (Interact.isInteract && !isDetaction)
        {
            isDetaction = true;
            detactionStartTime = Time.time;
        }
        else if (!Interact.isInteract && isDetaction)
            isDetaction = false;
        else if(isDetaction)
        {
            if(Time.time >= detactionStartTime + citizenData.detectionTime)
            {
                stateMachine.ChangeState(citizen.DetectionState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
