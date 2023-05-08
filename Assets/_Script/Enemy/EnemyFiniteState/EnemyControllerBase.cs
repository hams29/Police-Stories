using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerBase : MonoBehaviour
{
    #region State Variables
    public EnemyStateMachine stateMachine { get; protected set; }
    [SerializeField]
    protected EnemyData enemyData;
    #endregion

    #region Component
    public Rigidbody myRB { get; protected set; }
    public CapsuleCollider myColl { get; protected set; }
    public Animator Anim { get; protected set; }
    public Core Core { get; protected set; }

    protected States States { get => states ?? Core.GetCoreComponent(ref states); }
    private States states;
    #endregion

    protected virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        stateMachine = new EnemyStateMachine();
    }

    protected virtual void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();
        Anim = GetComponent<Animator>();
        
    }

    protected virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {

    }
}
