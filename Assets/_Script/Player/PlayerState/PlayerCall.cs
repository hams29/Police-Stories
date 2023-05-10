using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCall : PlayerState
{
    public PlayerCall(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        List<EnemyControllerBase> enemyList = player.search.enemyShowList;
        foreach (EnemyControllerBase controller in enemyList)
            controller.PlayerCall(player.transform.position);

        stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
