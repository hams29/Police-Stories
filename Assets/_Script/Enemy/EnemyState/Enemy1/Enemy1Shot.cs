using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Shot : EnemyState
{
    private float shotCount;
    public Enemy1Shot(Enemy1Controller enemy,EnemyStateMachine stateMachine,EnemyData enemyData,string animBoolName):base(enemy,stateMachine,enemyData,animBoolName)
    { }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        shotCount = 0;
        enemy.SetTrueSurrenderProbability();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if (gameManager.GameManager.isPlayerDead)
            stateMachine.ChangeState(enemy.IdleState);

        base.LogicUpdate();
        Gun gun = enemy.mainWeapon.GetComponent<Gun>();

        if (gun != null)
        {
            gun.Shot();
            if(gun.GetLastGunShot())
                shotCount++;
            if (!gun.GetFullAuto() || gun.GetCurrentMagazineAmmo() <= 0 || shotCount >= enemyData.enemyShotCount)
            {
                //stateMachine.ChangeState(enemy.IdleState);
                if (enemy.PlayerSearch.isPlayerFind)
                    stateMachine.ChangeState(enemy.ShotState);
                else
                    stateMachine.ChangeState(enemy.MoveLastPointState);
            }
            else
            {
                //ÉvÉåÉCÉÑÅ[ÇÃï˚å¸Ç…å¸Ç≠
                Rotation.SetRotation(enemy.PlayerSearch.playerPos);
            }
        }
        else
            Debug.LogError("Not Set Gun!!");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
