using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : PlayerState
{
    public PlayerDead(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName):base(player,stateMachine,playerData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Player is Dead");
        InputManagerDontDestroy.Instance.playerInput.SwitchCurrentActionMap(InputManagerDontDestroy.Instance.GetGameEndActionMapName());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            if(gameManager.GameManager != null)
            {
                if (!gameManager.GameManager.isPlayerDead)
                    gameManager.GameManager.PlayerDead();
            }    
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
