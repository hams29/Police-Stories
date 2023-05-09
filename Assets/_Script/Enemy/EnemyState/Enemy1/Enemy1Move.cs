using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy1Move : EnemyState
{
    private int nowLootCount;
    private int maxLootCount;
    private List<Vector3> enemyLootList = null;
    public Enemy1Move(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName):base(enemy,stateMachine,enemyData,animBoolName)
    {
        nowLootCount = 0;
        List<GameObject> lootObj = enemy.getMoveLoot();
        enemyLootList = new List<Vector3>(); // ��������ǉ�

        for (int i = 0; i < lootObj.Count;i++)
        {
            enemyLootList.Add(lootObj[i].transform.position);
        }
        maxLootCount = enemyLootList.Count;
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

        workspace = (enemyLootList[nowLootCount] - enemy.transform.position).normalized;
        Movement?.SetVelocity(workspace, enemyData.moveSpeed);

        Vector3 pos = enemy.transform.position;
        workspace = new Vector3(workspace.x + pos.x, workspace.y + pos.y, workspace.z + pos.z);
        Rotation.SetRotation(workspace);

        if (enemyLootList[nowLootCount].x + 0.1 > enemy.transform.position.x && enemyLootList[nowLootCount].x - 0.1 < enemy.transform.position.x)
        {
            if (enemyLootList[nowLootCount].z + 0.1 > enemy.transform.position.z && enemyLootList[nowLootCount].z - 0.1 < enemy.transform.position.z)
            {
                if (maxLootCount <= nowLootCount + 1)
                {
                    nowLootCount = 0;
                    enemy.IdleState.SetLockTime(Time.time + 2.0f);
                    stateMachine.ChangeState(enemy.IdleState);
                }
                else
                    nowLootCount++;
            }

        }

        if (enemy.PlayerSearch.isPlayerFind)
        {
            stateMachine.ChangeState(enemy.PlayerSearchState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}