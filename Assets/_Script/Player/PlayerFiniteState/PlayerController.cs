using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine stateMachine { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    //各ステータス
    public PlayerIdle IdleState { get; private set; }
    public PlayerMove MoveState { get; private set; }
    public PlayerRun RunState { get; private set; }
    public PlayerMelee MeleeState { get; private set; }
    #endregion

    #region Component
    public Rigidbody myRB { get; private set; }
    public CapsuleCollider myColl { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler inputController { get; private set; }
    public Core Core { get; private set; }
    #endregion

    #region Other Variables
    private Vector2 workspace;

    private Plane plane = new Plane();
    float distance = 0;
    #endregion

    #region Unity Callback Function
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdle(this, stateMachine, playerData, "idle");
        MoveState = new PlayerMove(this, stateMachine, playerData, "move");
        RunState = new PlayerRun(this, stateMachine, playerData, "run");
        MeleeState = new PlayerMelee(this, stateMachine, playerData, "melee");
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();
        inputController = GetComponent<PlayerInputHandler>();
        Anim = GetComponent<Animator>();

        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();

        var ray = Camera.main.ScreenPointToRay(inputController.MousePosition);
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if(plane.Raycast(ray,out distance))
        {
            var lookpoint = ray.GetPoint(distance);
            //TODO::Rotationで処理
            transform.LookAt(lookpoint);
        }

        //TODO::デバッグ用
        if (Input.GetKeyDown(KeyCode.P) && stateMachine.CurrentState == MeleeState)
            MeleeState.AnimationFinishTrigger();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Function
    private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}
