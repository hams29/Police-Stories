using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Controller : EnemyControllerBase
{
    #region State Variables
    [SerializeField]
    private GameObject handGunSet;

    [SerializeField]
    private GameObject assaultRifleSet;

    [SerializeField]
    private List<GameObject> moveLoot = new List<GameObject>();

    //各ステータス
    public Enemy1Idle IdleState { get; private set; }
    public Enemy1Death DeadState { get; private set; }
    public Enemy1Shot ShotState { get; private set; }
    public Enemy1PlayerSearch PlayerSearchState { get; private set; }
    public Enemy1Reload ReloadState { get; private set; }
    public Enemy1Move MoveState { get; private set; }
    public Enemy1Surrender SurrenderState { get; private set; }
    public Enemy1MoveLostPoint MoveLastPointState { get; private set; }
    public Enemy1Detection DetactionState { get; private set; }
    #endregion

    #region Component
    public Inventory Inventory { get; private set; }
    public GameObject mainWeapon { get; private set; }
    private Rotation Rotation { get => rotation ?? Core.GetCoreComponent(ref rotation); }
    private Rotation rotation;

    private Interact Interact { get => interact ?? Core.GetCoreComponent(ref interact); }
    private Interact interact;

    private Damage Damage { get => damage ?? Core.GetCoreComponent(ref damage); }
    private Damage damage;
    #endregion

    #region Other Variables
    private Vector3 workspace;

    public bool enemySurrenderProbability { get; private set; }

    [SerializeField]
    private Enemy1ScoreData enemyScoreData;

    public NavMeshAgent navAgent { get; private set; }
    #endregion


    #region Unity CallbackFunction
    protected override void Awake()
    {
        base.Awake();

        //moveLootに一つも入っていない場合
        if (moveLoot.Count <= 0)
        {
            GameObject loot = new GameObject("Loot1");
            loot.transform.parent = gameObject.transform;
            moveLoot.Add(loot);
        }
        else
        {
            if (moveLoot[0] == null)
            {
                moveLoot.Clear();
                GameObject loot = new GameObject("Loot1");
                loot.transform.parent = gameObject.transform;
                moveLoot.Add(loot);
            }
        }

        //各ステータスの初期化
        IdleState = new Enemy1Idle(this, stateMachine, enemyData, "idle", enemyScoreData);
        DeadState = new Enemy1Death(this, stateMachine, enemyData, "dead", enemyScoreData);
        ShotState = new Enemy1Shot(this, stateMachine, enemyData, "shot");
        PlayerSearchState = new Enemy1PlayerSearch(this, stateMachine, enemyData, "search", enemyScoreData);
        ReloadState = new Enemy1Reload(this, stateMachine, enemyData, "reload", enemyScoreData);
        MoveState = new Enemy1Move(this, stateMachine, enemyData, "move", enemyScoreData);
        SurrenderState = new Enemy1Surrender(this, stateMachine, enemyData, "surrender", enemyScoreData);
        MoveLastPointState = new Enemy1MoveLostPoint(this, stateMachine, enemyData, "moveLastPoint", enemyScoreData);
        DetactionState = new Enemy1Detection(this, stateMachine, enemyData, "detaction", enemyScoreData);
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

        navAgent = GetComponent<NavMeshAgent>();
        enemySurrenderProbability = false;
        mainWeapon = Instantiate(Inventory.mainWeapon, setMainWeapon.transform);
        States.SetInitHP(enemyData.maxHP);
        stateMachine.Initialize(IdleState);
        Interact.canInteract = false;
        navAgent.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        if(Damage.isDamage)
        {
            if(Damage.isMeleeDamage && stateMachine.CurrentState != SurrenderState)
            {
                Damage?.UseMeleeDamageHundler();
                stateMachine.ChangeState(SurrenderState);
            }
            Damage?.UseDamageHandler();
            Rotation?.SetRotation(Damage.shotAnyPos);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    #region Other Function
    private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    public override void PlayerCall(Vector3 ppos)
    {
        base.PlayerCall(ppos);

        if (enemySurrenderProbability)
            return;
        else
            enemySurrenderProbability = true;

        Rotation.SetRotation(ppos);
        float rand = Random.Range(0, 100.0f);
        //待機、歩き、プレイヤー探知ステータスのみ降伏するようにする
        if ((stateMachine.CurrentState == IdleState || stateMachine.CurrentState == MoveState || stateMachine.CurrentState == PlayerSearchState) && rand <= enemyData.surrenderProbability)
        {
            stateMachine.ChangeState(SurrenderState);
        }
        else
        {
            IdleState.SetLockTime(0.5f);
            stateMachine.ChangeState(IdleState);
        }
    }

    public List<GameObject> getMoveLoot() { return moveLoot; }

    public void SetTrueSurrenderProbability() { enemySurrenderProbability = true; }
    #endregion
}
