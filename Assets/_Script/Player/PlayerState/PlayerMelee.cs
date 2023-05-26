using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : PlayerState
{
    private bool meleeAttack;
    private bool meleeHit;

    public PlayerMelee(PlayerController player,PlayerStateMachine stateMachine, PlayerData playerData,string animBoolName):base(player,stateMachine,playerData,animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = false;
        meleeAttack = false;
        Movement?.SetVelocityZero();
        meleeHit = false;
    }

    public override void Exit()
    {
        base.Exit();

        player.inputController.UseMeleeInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //近接攻撃の処理
        if(meleeAttack && !meleeHit)
        {
            //RaycastHit hitObject;
            Vector3 pos = new(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
            if (Physics.Raycast(pos , player.transform.forward, out RaycastHit hitObject,playerData.meleeDistance))
            {
                Core otherCore = hitObject.transform.GetComponentInChildren<Core>();
                if(otherCore != null)
                {
                    Damage otherDamage = null;
                    otherCore.GetCoreComponent(ref otherDamage);
                    if(otherDamage != null)
                    {
                        otherDamage.addMeleeDamage(playerData.meleeDamage);
                        otherDamage.SetMeleeAnyPos(player.transform.position);
                        meleeHit = true;
                        Debug.Log("Melee Damage!!");
                    }
                }
            }
        }

        //近接攻撃モーション終了時次のステータスに移行
        if(isAnimationFinished)
        {
            if (xInput != 0 || zInput != 0)
            {
                if (dashinput)
                    stateMachine.ChangeState(player.RunState);
                else
                    stateMachine.ChangeState(player.MoveState);
            }
            else
                stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        meleeAttack = !meleeAttack;
    }
}
