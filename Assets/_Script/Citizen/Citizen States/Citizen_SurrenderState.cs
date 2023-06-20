using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen_SurrenderState : CitizenState
{
    public Citizen_SurrenderState(CitizenController citizen,CitizenStateMachine stateMachine,CitizenData citizenData,string animBoolName):base(citizen,stateMachine,citizenData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        gameManager.GameManager?.AddScore(50.0f);
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
