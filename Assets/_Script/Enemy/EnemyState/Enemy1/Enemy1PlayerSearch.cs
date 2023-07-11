using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1PlayerSearch : EnemyState
{
    private Gun gun;
    private Enemy1ScoreData scoreData;
    public Enemy1PlayerSearch(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy1ScoreData scoreData) : base(enemy, stateMachine, enemyData, animBoolName)
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
        Movement?.SetVelocityZero();
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
                    Debug.Log("Enemy1Search");
                    ScoreMessage.scoreMessage?.TextInMsg();
                }
            }
            else
            {
                if (gameManager.GameManager != null)
                {
                    gameManager.GameManager.AddScore(-10.0f);
                    gameManager.GameManager.SetScorePM(false);
                    gameManager.GameManager.SetScoreMsg("敵にダメージ");
                    Debug.Log("Enemy1Search");
                    ScoreMessage.scoreMessage?.TextInMsg();
                }
            }
        }

        //プレイヤーの方向に向く
        Rotation.SetRotation(enemy.PlayerSearch.SearchPos);
        if (gun.GetCurrentMagazineAmmo() <= 0)
        {
            stateMachine.ChangeState(enemy.ReloadState);
        }
        else if (Time.time > enemyData.playerSearchTime + startTIme && !enemy.PlayerSearch.isSearchDead)
        {
            stateMachine.ChangeState(enemy.ShotState);
        }

        //プレイヤーを見失った際にMoveLastPointステータスに移行
        if (!enemy.PlayerSearch.isSearchFind)
            stateMachine.ChangeState(enemy.MoveLastPointState);

        if (enemy.isHerePlayerShotSound)
        {
            enemy.UseHerePlayerShotSound();
            enemy.IdleState.SetLockTime(2.0f);
            enemy.IdleState.SetNextState(enemy.MoveHerePointState);
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
