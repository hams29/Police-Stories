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
    public Citizen_DetectionState DetectionState { get; private set; }
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
    private List<Renderer> renderers = new List<Renderer>();
    #endregion

    #region Unity CallbackFunction
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        stateMachine = new CitizenStateMachine();

        IdleState = new Citizen_IdleState(this, stateMachine, citizenData, "Idle");
        SurrenderState = new Citizen_SurrenderState(this, stateMachine, citizenData, "Surrender");
        DetectionState = new Citizen_DetectionState(this, stateMachine, citizenData, "Detection");

    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();
        Anim = GetComponent<Animator>();

        stateMachine.Initialize(IdleState);
        Renderer[] material = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < material.Length; i++)
            renderers.Add(material[i]);

        Show?.InitMaterials(renderers);
        gameManager.GameManager?.addMaxEnemy();
    }

    private void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();

        if(Damage.isDamage)
        {
            //TODO::ƒ_ƒ[ƒWˆ—
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
        Rotation.SetRotation(ppos);
        if (stateMachine.CurrentState != SurrenderState && stateMachine.CurrentState != DetectionState)
        {
            stateMachine.ChangeState(SurrenderState);
        }
    }
    #endregion
}
