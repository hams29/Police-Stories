using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : PlayerState
{
    private bool canShot;
    public PlayerShot(PlayerController player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName):base(player,stateMachine,playerData,animBoolName)
    { }

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
        Gun gun = player.mainWeapon.GetComponent<Gun>();

        if (gun != null)
        {
            gun.Shot();
            if(!gun.GetFullAuto() || gun.GetCurrentMagazineAmmo() <= 0 || !shotInput)
            {
                player.inputController.UseShotInput();
                stateMachine.ChangeState(player.IdleState);
            }
        }
        else
            Debug.LogError("Not Set Gun!!");


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        workspace = new Vector3(xInput, 0, zInput);
        Movement?.SetVelocity(workspace, playerData.moveSpeed);
    }
}
