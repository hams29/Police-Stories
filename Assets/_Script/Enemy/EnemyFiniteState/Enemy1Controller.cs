using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    #region State Variables
    public EnemyStateMachine stateMachine { get; private set; }
    [SerializeField]
    private EnemyData enemyData;

    //各ステータス

    public Enemy1Idle IdleState { get; private set; }
    public Enemy1Death DeadState { get; private set; }
    #endregion


    #region Component
    public Rigidbody myRB { get; private set; }
    public CapsuleCollider myColl { get; private set; }
    public Animator Anim { get; private set; }
    public Core Core { get; private set; }

    private States States { get => states ?? Core.GetCoreComponent(ref states); }
    private States states;
    #endregion


    #region Other Variables
    private Vector3 workspace;
    #endregion


    #region Unity CallbackFunction
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        stateMachine = new EnemyStateMachine();
        States.SetInitHP(enemyData.maxHP);

        //各ステータスの初期化
        IdleState = new Enemy1Idle(this, stateMachine, enemyData, "idle");
        DeadState = new Enemy1Death(this, stateMachine, enemyData, "dead");
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();
        Anim = GetComponent<Animator>();

        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();
    }
    #endregion
}
