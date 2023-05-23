using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Detection : EnemyState
{
    public Enemy1Detection(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName):base(enemy,stateMachine,enemyData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        Interact.canInteract = false;
        if (gameManager.GameManager != null)
            gameManager.GameManager.AddScore(100.0f);
        Debug.Log(enemy.name + " ÇçSë©");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Damage.isDamage)
        {
            if (gameManager.GameManager != null)
                gameManager.GameManager.AddScore(-100.0f);
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
