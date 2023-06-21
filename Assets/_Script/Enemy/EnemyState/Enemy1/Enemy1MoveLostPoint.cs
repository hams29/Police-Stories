using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1MoveLostPoint : EnemyState
{
    private Enemy1ScoreData scoreData;
    public Enemy1MoveLostPoint(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName,Enemy1ScoreData scoreData):base(enemy,stateMachine,enemyData,animBoolName)
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

        Vector3 lastPlayerPos = new Vector3(enemy.PlayerSearch.playerPos.x, 0, enemy.PlayerSearch.playerPos.z);
        //Vector3 pos = new Vector3(enemy.transform.position.x, 0, enemy.transform.position.z);
        //workspace = (lastPlayerPos - pos).normalized;
        workspace = new Vector3(lastPlayerPos.x - enemy.transform.position.x,
                                0,
                                lastPlayerPos.z - enemy.transform.position.z);
        Movement?.SetVelocity(workspace, enemyData.runSpeed);
        Vector3 pos = enemy.transform.position;
        workspace = new Vector3(workspace.x + pos.x, pos.y, workspace.z + pos.z);
        Rotation?.SetRotation(workspace);

        //áŠQ•¨‚É“–‚½‚Á‚½Û‚Ìˆ—
        float detectionDistance = 0.5f;
        float detectionHeight = 0.2f;

        Vector3 rpos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1.5f, enemy.transform.position.z);
        // ¶‰E‚ÌáŠQ•¨‚ÌŒŸ’m
        RaycastHit leftHit;
        bool hasLeftObstacle = Physics.BoxCast(rpos, new Vector3(detectionDistance, detectionHeight, detectionDistance),
            -enemy.transform.right, out leftHit, Quaternion.identity, detectionDistance);

        RaycastHit rightHit;
        bool hasRightObstacle = Physics.BoxCast(rpos, new Vector3(detectionDistance, detectionHeight, detectionDistance),
            enemy.transform.right, out rightHit, Quaternion.identity, detectionDistance);

        // ‘O•û‚ÌáŠQ•¨‚ÌŒŸ’m
        RaycastHit forwardHit;
        bool hasForwardObstacle = Physics.BoxCast(rpos, new Vector3(detectionDistance, detectionHeight, detectionDistance),
            enemy.transform.forward, out forwardHit, Quaternion.identity, detectionDistance);

        if((hasLeftObstacle && leftHit.transform.tag != "Player") ||
           (hasRightObstacle && rightHit.transform.tag != "Player") ||
           (hasForwardObstacle && forwardHit.transform.tag != "Player"))
        {
            enemy.IdleState.SetLockTime(2.0f);
            stateMachine.ChangeState(enemy.IdleState);
        }


        if (enemy.PlayerSearch.isPlayerFind)
        {
            stateMachine.ChangeState(enemy.PlayerSearchState);
        }

        if(lastPlayerPos.x + 0.1 > pos.x && lastPlayerPos.x - 0.1 < pos.x)
        {
            if(lastPlayerPos.z + 0.1 > pos.z && lastPlayerPos.z - 0.1 < pos.z)
            {
                enemy.IdleState.SetLockTime(2.0f);
                stateMachine.ChangeState(enemy.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
