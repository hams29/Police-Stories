using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenController : MonoBehaviour
{
    #region State Variables
    public CitizenStateMachine stateMachine { get; private set; }
    [SerializeField]
    private CitizenData citizenData;
    #endregion

    #region Component
    public Rigidbody myRB { get; private set; }
    public CapsuleCollider myColl { get; private set; }
    public Animator Anim { get; private set; }
    public Core Core { get; private set; }
    #endregion

    #region Other Variables
    #endregion

    #region Unity CallbackFunction
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        stateMachine = new CitizenStateMachine();
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();   
    }

    #endregion

    #region Other Function
    #endregion
}
