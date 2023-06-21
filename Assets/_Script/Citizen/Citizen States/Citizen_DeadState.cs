using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen_DeadState : CitizenState
{
    public Citizen_DeadState(CitizenController citizen,CitizenStateMachine stateMachine,CitizenData citizenData,string animBoolName):base(citizen,stateMachine,citizenData,animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        if (stateMachine.OldState != citizen.DetectionState)
            gameManager.GameManager?.addEliminatedEnemy();

        gameManager.GameManager?.AddScore(-100.0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
