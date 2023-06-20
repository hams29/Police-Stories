using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour
{
    #region State Variables
    public CitizenStateMachine stateMachine { get; private set; }
    [SerializeField]
    private CitizenData citizenData;

    public Citizen_IdleState IdleState { get; private set; }
    public Citizen_SurrenderState SurrenderState { get; private set; }
    #endregion

    #region Component
    public Rigidbody myRB { get; private set; }
    public CapsuleCollider myColl { get; private set; }
    public Animator Anim { get; private set; }
    public Core Core { get; private set; }

    public Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }
    public Rotation Rotation { get => rotation ?? Core.GetCoreComponent(ref rotation); }
    public Damage Damage { get => damage ?? Core.GetCoreComponent(ref damage); }
    public States States { get => states ?? Core.GetCoreComponent(ref states); }
    public Show Show { get => show ?? Core.GetCoreComponent(ref show); }
    public Interact Interact { get => interact ?? Core.GetCoreComponent(ref interact); }

    private Damage damage;
    private Movement movement;
    private Rotation rotation;
    private States states;
    private Show show;
    private Interact interact;
    #endregion

    #region Other Variables
    #endregion

    #region Unity CallbackFunction
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        stateMachine = new CitizenStateMachine();

        IdleState = new Citizen_IdleState(this, stateMachine, citizenData, "Idle");
        SurrenderState = new Citizen_SurrenderState(this, stateMachine, citizenData, "Surrender");

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

        if(Damage.isDamage)
        {
            //TODO::ダメージ処理
            Damage?.UseDamageHandler();
            Rotation?.SetRotation(Damage.shotAnyPos);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();   
    }

    #endregion

    #region Other Function
    public void PlayerCall(Vector3 ppos)
    {
        //TODO::プレイヤーから市民への降参呼びかけ

    }
    #endregion
}
