using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyControllerBase
{
    #region State Variables
    //各ステータス
    public Enemy1Idle IdleState { get; private set; }
    public Enemy1Death DeadState { get; private set; }
    #endregion

    #region Other Variables
    private Vector3 workspace;
    #endregion


    #region Unity CallbackFunction
    protected override void Awake()
    {
        base.Awake();

        //各ステータスの初期化
        IdleState = new Enemy1Idle(this, stateMachine, enemyData, "idle");
        DeadState = new Enemy1Death(this, stateMachine, enemyData, "dead");
    }

    protected override void Start()
    {
        base.Start();

        States.SetInitHP(enemyData.maxHP);
        stateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion
}
