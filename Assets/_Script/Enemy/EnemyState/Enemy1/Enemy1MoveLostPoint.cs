using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1MoveLostPoint : EnemyState
{
    private Enemy1ScoreData scoreData;
    public Enemy1MoveLostPoint(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy1ScoreData scoreData) : base(enemy, stateMachine, enemyData, animBoolName)
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

        enemy.navAgent.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.navAgent.enabled = false;
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
                    Debug.Log("Enemy1MovePoint");
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
                    Debug.Log("Enemy1MovePoint");
                    ScoreMessage.scoreMessage?.TextInMsg();
                }
            }
        }
        #region old
        /*
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

        //障害物に当たった際の処理
        float detectionDistance = 0.5f;
        float detectionHeight = 0.2f;

        Vector3 rpos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1.5f, enemy.transform.position.z);
        // 左右の障害物の検知
        RaycastHit leftHit;
        bool hasLeftObstacle = Physics.BoxCast(rpos, new Vector3(detectionDistance, detectionHeight, detectionDistance),
            -enemy.transform.right, out leftHit, Quaternion.identity, detectionDistance);

        RaycastHit rightHit;
        bool hasRightObstacle = Physics.BoxCast(rpos, new Vector3(detectionDistance, detectionHeight, detectionDistance),
            enemy.transform.right, out rightHit, Quaternion.identity, detectionDistance);

        // 前方の障害物の検知
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
        */
        #endregion
        #region new
        Vector3 lastPlayerPos = new Vector3(enemy.PlayerSearch.playerPos.x, 0, enemy.PlayerSearch.playerPos.z);
        enemy.navAgent.SetDestination(lastPlayerPos);

        if (lastPlayerPos.x + 0.1 > enemy.transform.position.x && lastPlayerPos.x - 0.1 < enemy.transform.position.x)
        {
            if (lastPlayerPos.z + 0.1 > enemy.transform.position.z && lastPlayerPos.z - 0.1 < enemy.transform.position.z)
            {
                enemy.IdleState.SetLockTime(2.0f);
                enemy.IdleState.SetNextState(enemy.RemoveNormalLootState);
                stateMachine.ChangeState(enemy.IdleState);
            }
        }

        //ドアを開ける処理（閉まっている時は何もしない）
        RaycastHit hitObject;
        Vector3 pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1.5f, enemy.transform.position.z);
        if (Physics.Raycast(pos, enemy.transform.forward, out hitObject, enemyData.interactDistance))
        {
            int layerNo = LayerMask.NameToLayer(enemyData.interactLayerName);
            if (hitObject.transform.gameObject.layer == layerNo)
            {
                if (DoorCheck(hitObject.transform.gameObject))
                {
                    Core otherCore = hitObject.transform.GetComponentInChildren<Core>();
                    if (otherCore != null)
                    {
                        Interact otherInteract = null;
                        otherCore.GetCoreComponent(ref otherInteract);
                        if (otherInteract != null)
                        {
                            if (otherInteract.canInteract)
                                otherInteract.SetInteract();
                        }
                    }
                }
            }
        }

        if (enemy.PlayerSearch.isPlayerFind)
            stateMachine.ChangeState(enemy.PlayerSearchState);
        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
