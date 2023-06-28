using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Detection : EnemyState
{
    private Enemy1ScoreData scoreData;
    public Enemy1Detection(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy1ScoreData scoreData) : base(enemy, stateMachine, enemyData, animBoolName)
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
        Interact.canInteract = false;
        if (gameManager.GameManager != null)
        {
            gameManager.GameManager.AddScore(100.0f);
            gameManager.GameManager.SetScorePM(true);
            gameManager.GameManager.SetScoreMsg("“G‚ÌS‘©‚É¬Œ÷");
            gameManager.GameManager.addEliminatedEnemy();
            Debug.Log("Enemy1Detection");


            ScoreMessage.scoreMessage?.TextInMsg();
        }
        Debug.Log(enemy.name + " ‚ğS‘©");
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
            {
                gameManager.GameManager.AddScore(-100.0f);
                gameManager.GameManager.SetScorePM(false);
                gameManager.GameManager.SetScoreMsg("“G‚ªƒ_ƒ[ƒW‚ğó‚¯‚½");
                Debug.Log("Enemy1Detection");
                ScoreMessage.scoreMessage?.TextInMsg();
            }
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
