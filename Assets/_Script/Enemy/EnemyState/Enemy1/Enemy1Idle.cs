using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Idle : EnemyState
{
    private Gun gun;
    private float lockTime = 0;

    private Enemy1ScoreData scoreData;
    private bool nextStateFlg;
    private EnemyState nextState;

    public Enemy1Idle(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy1ScoreData scoreData) : base(enemy, stateMachine, enemyData, animBoolName)
    {
        this.scoreData = scoreData;
        nextStateFlg = false;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        gun = enemy.mainWeapon.GetComponent<Gun>();
        Movement?.SetVelocityZero();
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
            if(enemy.enemySurrenderProbability)
            {
                if (gameManager.GameManager != null)
                {
                    gameManager.GameManager.AddScore(10.0f);
                    gameManager.GameManager.SetScorePM(true);
                    gameManager.GameManager.SetScoreMsg("çSë©íÜÇÃìGÇ…É_ÉÅÅ[ÉW");
                    Debug.Log("Enemy1Idle");
                    ScoreMessage.scoreMessage?.TextInMsg();
                }
            }
            else
            {
                if (gameManager.GameManager != null)
                {
                    gameManager.GameManager.AddScore(-10.0f);
                    gameManager.GameManager.SetScorePM(false);
                    gameManager.GameManager.SetScoreMsg("çSë©íÜÇÃìGÇ…É_ÉÅÅ[ÉW");
                    Debug.Log("Enemy1Idle");
                    ScoreMessage.scoreMessage?.TextInMsg();
                }
            }
        }

        if(enemy.isHerePlayerShotSound)
        {
            enemy.UseHerePlayerShotSound();
            SetLockTime(2.0f);
            SetNextState(enemy.MoveHerePointState);
        }

        if (Time.time < lockTime)
            return;
        if (nextStateFlg)
        {
            nextStateFlg = false;
            stateMachine.ChangeState(nextState);
        }
        else if (gun.GetCurrentMagazineAmmo() <= 0)
        {
            stateMachine.ChangeState(enemy.ReloadState);
        }
        else if (enemy.PlayerSearch.isPlayerFind)
        {
            stateMachine.ChangeState(enemy.PlayerSearchState);
        }
        else
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetLockTime(float time) { lockTime =  Time.time + time; }

    public void SetNextState(EnemyState state)
    {
        nextStateFlg = true;
        nextState = state;
    }
}
