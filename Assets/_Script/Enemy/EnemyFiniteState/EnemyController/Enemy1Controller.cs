using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : EnemyControllerBase
{
    #region State Variables
    [SerializeField]
    private GameObject handGunSet;

    [SerializeField]
    private GameObject assaultRifleSet;

    //各ステータス
    public Enemy1Idle IdleState { get; private set; }
    public Enemy1Death DeadState { get; private set; }
    public Enemy1Shot ShotState { get; private set; }
    public Enemy1PlayerSearch PlayerSearchState { get; private set; }
    public Enemy1Reload ReloadState { get; private set; }
    #endregion

    #region Component
    public Inventory Inventory { get; private set; }
    public GameObject mainWeapon { get; private set; }
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
        ShotState = new Enemy1Shot(this, stateMachine, enemyData, "shot");
        PlayerSearchState = new Enemy1PlayerSearch(this, stateMachine, enemyData, "search");
        ReloadState = new Enemy1Reload(this, stateMachine, enemyData, "reload");
    }

    protected override void Start()
    {
        base.Start();

        Inventory = GetComponentInChildren<Inventory>();

        GameObject setMainWeapon = null;
        switch (Inventory.gunType)
        {
            case mainWeaponData.GunType.HandGun:
                setMainWeapon = handGunSet;
                break;

            case mainWeaponData.GunType.AssaultRifle:
                setMainWeapon = assaultRifleSet;
                break;
        }

        mainWeapon = Instantiate(Inventory.mainWeapon, setMainWeapon.transform);
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

    #region Other Function
    private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}
