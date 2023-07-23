using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Reload : EnemyState
{
    private Gun gun;
    private Enemy1ScoreData scoreData;
    public Enemy1Reload(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy1ScoreData scoreData) : base(enemy, stateMachine, enemyData, animBoolName)
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
                {
                    gameManager.GameManager.AddScore(10.0f);
                    gameManager.GameManager.SetScorePM(true);
                    gameManager.GameManager.SetScoreMsg("敵にダメージ");
                    Debug.Log("Enemy1Reload");

                    ScoreMessage.scoreMessage.TextInMsg();

                }
            }
            else
            {
                if (gameManager.GameManager != null)
                {
                    gameManager.GameManager.AddScore(-10.0f);
                    gameManager.GameManager.SetScorePM(false);
                    gameManager.GameManager.SetScoreMsg("敵にダメージ");
                    Debug.Log("Enemy1Reload");
                    ScoreMessage.scoreMessage?.TextInMsg();
                }
            }
        }

        if (isAnimationFinishTrigger)
        {
            gun.MaxAmmo();
            stateMachine.ChangeState(enemy.IdleState);
        }

        if (States?.nowWeakening == States.WeakeningState.FrashBang)
        {
            stateMachine.ChangeState(enemy.StunState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
