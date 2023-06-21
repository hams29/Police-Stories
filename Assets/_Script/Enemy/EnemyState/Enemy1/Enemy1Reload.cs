using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Reload : EnemyState
{
    private Gun gun;
    private Enemy1ScoreData scoreData;
    public Enemy1Reload(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName,Enemy1ScoreData scoreData):base(enemy,stateMachine,enemyData,animBoolName)
    {
        this.scoreData = scoreData;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }
    public override void Enter()
    {
        base.Enter();
        gun = enemy.mainWeapon.GetComponent<Gun>(); 
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Damage.isDamage)
        {
            if (enemy.enemySurrenderProbability)
            {
                if (gameManager.GameManager != null)
                    gameManager.GameManager.AddScore(scoreData.enemyAddShotScore);
            }
            else
            {
                if (gameManager.GameManager != null)
                    gameManager.GameManager.AddScore(scoreData.enemySubShotScore);
            }
        }

        if (isAnimationFinishTrigger)
        {
            gun.MaxAmmo();
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
