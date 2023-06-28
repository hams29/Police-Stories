using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1RemoveNormalLoot : EnemyState
{

    private Enemy1ScoreData scoreData;
    public Enemy1RemoveNormalLoot(Enemy1Controller enemy, EnemyStateMachine stateMachine, EnemyData enemyData, string animBoolName, Enemy1ScoreData scoreData) : base(enemy, stateMachine, enemyData, animBoolName)
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
        enemy.footImageManager.FootImageStart();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.navAgent.enabled = false;
        enemy.footImageManager.FootImageStop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.footImageManager.SetFootImagePosition(enemy.transform.position);

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

        //�h�A���J���鏈���i�܂��Ă��鎞�͉������Ȃ��j
        RaycastHit hitObject;
        Vector3 pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1.5f, enemy.transform.position.z);
        if (Physics.Raycast(pos, enemy.transform.forward, out hitObject, enemyData.interactDistance))
        {
            int layerNo = LayerMask.NameToLayer(enemyData.interactLayerName);
            if (hitObject.transform.gameObject.layer == layerNo)
            {
                if(DoorCheck(hitObject.transform.gameObject))
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

        workspace = enemy.MoveState.enemyLootList[enemy.MoveState.nowLootCount];
        enemy.navAgent.SetDestination(workspace);

        if (enemy.transform.position.x - workspace.x <= 0.1f && enemy.transform.position.x - workspace.x >= -0.1f)
        {
            if (enemy.transform.position.z - workspace.z <= 0.1f && enemy.transform.position.z - workspace.z >= -0.1f)
            {
                if (enemy.MoveState.nowLootCount + 1 >= enemy.MoveState.maxLootCount)
                {
                    enemy.MoveState.SetNowLootCount(0);
                    enemy.IdleState.SetLockTime(2.0f);
                    stateMachine.ChangeState(enemy.IdleState);
                }
                else
                {
                    enemy.MoveState.SetNowLootCount(enemy.MoveState.nowLootCount);
                    stateMachine.ChangeState(enemy.MoveState);
                }
            }
        }

        if (enemy.PlayerSearch.isPlayerFind)
            stateMachine.ChangeState(enemy.PlayerSearchState);

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