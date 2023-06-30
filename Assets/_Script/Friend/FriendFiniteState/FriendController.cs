using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    #region State Variables
    public FriendStateMachine stateMachine { get; private set; }

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
    public FunSearch search { get; private set; }

    private States States { get => states ?? Core.GetCoreComponent(ref states); }
    private States states;
    private Rotation Rotation { get => rotation ?? Core.GetCoreComponent(ref rotation); }
    private Rotation rotation;
    private Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }
    private Movement movement;
    #endregion

    #region Other Variables
    private Vector3 workspace;

    //private Plane plane = new Plane();
    //private float distance = 0;
    #endregion

    #region Unity Callback Function
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        stateMachine = new FriendStateMachine();
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();

        Anim = GetComponent<Animator>();
        Inventory = GetComponentInChildren<Inventory>();
        search = GetComponentInChildren<FunSearch>();

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
        //stateMachine.Initialize(IdleState);
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
    #endregion
}
