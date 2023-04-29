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
    public PlayerShot ShotState { get; private set; }
    public PlayerReload ReloadState { get; private set; }
    public PlayerDead DeadState { get; private set; }
    #endregion

    #region Component
    public Rigidbody myRB { get; private set; }
    public CapsuleCollider myColl { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler inputController { get; private set; }
    public Core Core { get; private set; }
    public Inventory Inventory { get; private set; }
    public GameObject mainWeapon { get; private set; }
    public Gun gun { get; private set; }
    
    private States States { get => states ?? Core.GetCoreComponent(ref states); }
    private States states;
    private Rotation Rotation { get => rotation ?? Core.GetCoreComponent(ref rotation); }
    private Rotation rotation;
    #endregion

    #region Other Variables
    private Vector3 workspace;

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
        ShotState = new PlayerShot(this, stateMachine, playerData, "shot");
        ReloadState = new PlayerReload(this, stateMachine, playerData, "reload");
        DeadState = new PlayerDead(this, stateMachine, playerData, "dead");
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();
        inputController = GetComponent<PlayerInputHandler>();
        Anim = GetComponent<Animator>();
        Inventory = GetComponentInChildren<Inventory>();
        mainWeapon = Instantiate(Inventory.mainWeapon, GameObject.Find("handGunSet").transform);
        gun = mainWeapon.GetComponent<Gun>();

        stateMachine.Initialize(IdleState);
        States.SetInitHP(playerData.maxHP);
    }

    private void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();


        //マウスの位置が正面になるようにプレイヤーを回転させる処理
        {
            var ray = Camera.main.ScreenPointToRay(inputController.MousePosition);
            plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            if(plane.Raycast(ray,out distance) && stateMachine.CurrentState != RunState)
            {
                Vector3 lookpoint = ray.GetPoint(distance);
                Rotation.SetRotation(lookpoint);
            }

        }

        //Animatorに必要な値を入れる処理
        AnimationInputValueSet();

        //TODO::デバッグ用::後で削除
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                States.addDamage(20.0f);
                Debug.Log("currentHP is " + States.currentHP);
            }
            else if(Input.GetKeyUp(KeyCode.DownArrow))
            {
                States.addHealth(20.0f);
                Debug.Log("currentHP is " + States.currentHP);
            }            
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Debug.DrawRay(pos, gameObject.transform.forward.normalized * playerData.meleeDistance, Color.blue, 0.5f);
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Function
    private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    private void AnimationInputValueSet()
    {
        //TODO::PlayerCOntroller::プレイヤーの向きによってアニメーションの変更
        Vector3 forward = this.gameObject.transform.forward;
        forward = new Vector3(forward.x, 0, forward.z).normalized;

        float inputForward = inputController.NormInputZ * forward.z;
        float inputRight = inputController.NormInputX * forward.x;

        Anim.SetFloat("inputForward", inputForward);
        Anim.SetFloat("inputRight", inputRight);

        Anim.SetFloat("playerDirectionX", this.gameObject.transform.forward.x);
        Anim.SetFloat("playerDirectionZ", this.gameObject.transform.forward.z);
    }
    #endregion
}
