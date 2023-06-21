using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen_DetectionState : CitizenState
{
    public Citizen_DetectionState(CitizenController citizen, CitizenStateMachine stateMachine, CitizenData citizenData, string animBoolName) : base(citizen, stateMachine, citizenData, animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        Interact.canInteract = false;
        gameManager.GameManager?.AddScore(100.0f);
        gameManager.GameManager?.addEliminatedEnemy();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Damage.isDamage)
            gameManager.GameManager?.AddScore(-100.0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
