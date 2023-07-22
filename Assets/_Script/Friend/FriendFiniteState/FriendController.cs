using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendController : MonoBehaviour
{
    #region State Variables
    public FriendStateMachine stateMachine { get; private set; }

    public FriendIdleState IdleState { get; private set; }
    public FriendMoveState MoveState { get; private set; }
    public FriendMovePointState MovePointState { get; private set; }
    public FriendOpenDoor OpenDoorState { get; private set; }
    public FriendDetected DetectedState { get; private set; }
    public FriendShotState ShotState { get; private set; }
    public FriendReloadState ReloadState { get; private set; }
    public FriendDeadState DeadState { get; private set; }
    public FriendStunState StunState { get; private set; }

    [SerializeField]
    private FriendData friendData;

    [SerializeField]
    private GameObject handGunSet;

    [SerializeField]
    private GameObject assaultRifleSet;
    #endregion

    #region Component
    public Rigidbody myRB { get; private set; }
    public CapsuleCollider myColl { get; private set; }
    public Animator Anim { get; private set; }
    public Core Core { get; private set; }
    public Inventory Inventory { get; private set; }
    //public GameObject mainWeapon { get; private set; }
    public Gun gun { get; private set; }
    public FriendSearch search { get; private set; }

    private States States { get => states ?? Core.GetCoreComponent(ref states); }
    private States states;
    private Rotation Rotation { get => rotation ?? Core.GetCoreComponent(ref rotation); }
    private Rotation rotation;
    private Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }
    private Movement movement;
    #endregion

    #region Other Variables
    private Vector3 workspace;

    public bool isFollow { get; private set; }
    public NavMeshAgent navAgent { get; private set; }
    public Vector3 turgetPosition { get; private set; }

    public enum SendAction
    {
        Follow,
        Move,
        Stop,
        OpenDoor,
        ThrowFrashBang,
        None
    }

    #endregion

    #region Unity Callback Function
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        stateMachine = new FriendStateMachine();

        IdleState = new FriendIdleState(this, stateMachine, friendData, "idle");
        MoveState = new FriendMoveState(this, stateMachine, friendData, "move");
        MovePointState = new FriendMovePointState(this, stateMachine, friendData, "movePoint");
        OpenDoorState = new FriendOpenDoor(this, stateMachine, friendData, "openDoor");
        DetectedState = new FriendDetected(this, stateMachine, friendData, "detected");
        ShotState = new FriendShotState(this, stateMachine, friendData, "shot");
        ReloadState = new FriendReloadState(this, stateMachine, friendData, "reload");
        DeadState = new FriendDeadState(this, stateMachine, friendData, "dead");
        StunState = new FriendStunState(this, stateMachine, friendData, "stun");
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();

        Anim = GetComponent<Animator>();
        Inventory = GetComponentInChildren<Inventory>();
        search = GetComponentInChildren<FriendSearch>();
        navAgent = GetComponent<NavMeshAgent>();

        Inventory.SetMainWeapon();
        Inventory.SetGadget();

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

        //mainWeapon = Instantiate(Inventory.mainWeapon, setMainWeapon.transform);
        this.Inventory.SetMainWeapon(Instantiate(Inventory.mainWeapon, setMainWeapon.transform));
        gun = this.Inventory.mainWeapon.GetComponent<Gun>();
        isFollow = true;
        stateMachine.Initialize(IdleState);
        navAgent.enabled = false;
        gameManager.GameManager?.SetFriend(this);
        States?.SetInitHP(friendData.maxHP);
    }

    private void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();
        bool isClear = false;
        if (gameManager.GameManager != null)
            isClear = gameManager.GameManager.isGameClear;

        //�}�E�X�̈ʒu�����ʂɂȂ�悤�Ƀv���C���[����]�����鏈��
        if (!States.dead && !isClear)
        {
        }
        else if (isClear)
        {
            //stateMachine.ChangeState(IdleState);
            stateMachine.canChangeState = false;
            Movement.CanSetVelocity = false;
            Rotation.CanSetRotate = false;
        }
        
        if(States.dead && stateMachine.CurrentState != DeadState)
        {
            stateMachine.ChangeState(DeadState);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Function
    private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    public bool CheckFrontObject(string tag, out GameObject gameObject, float distance)
    {
        RaycastHit hitObject;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        if (Physics.Raycast(pos, transform.forward, out hitObject, distance))
        {
            if (hitObject.transform.tag == tag)
            {
                gameObject = hitObject.transform.gameObject;
                return true;
            }
        }

        gameObject = null;
        return false;
    }

    public bool CheckFrontObject(string tag, float distance)
    {
        RaycastHit hitObject;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        if (Physics.Raycast(pos, transform.forward, out hitObject, distance))
        {
            if (hitObject.transform.tag == tag)
            {
                return true;
            }
        }
        return false;
    }

    public void SetFollow(bool flg) => isFollow = flg;
    public void ReceiveAction(SendAction action)
    {
        switch(action)
        {
            case SendAction.Follow:
                isFollow = true;
                break;
            case SendAction.Move:
                isFollow = false;
                stateMachine.ChangeState(MovePointState);
                break;
            case SendAction.Stop:
                isFollow = false;
                break;
            case SendAction.OpenDoor:
                stateMachine.ChangeState(OpenDoorState);
                break;
            case SendAction.ThrowFrashBang:
                break;
            default:
                break;
        }
    }

    public void SetTurgetPosition(Vector3 tpos) { turgetPosition = tpos; }
    #endregion
}
