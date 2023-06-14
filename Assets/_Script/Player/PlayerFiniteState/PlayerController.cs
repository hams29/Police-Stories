using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine stateMachine { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private GameObject handGunSet;

    [SerializeField]
    private GameObject assaultRifleSet;

    //�e�X�e�[�^�X
    public PlayerIdle IdleState { get; private set; }
    public PlayerMove MoveState { get; private set; }
    public PlayerRun RunState { get; private set; }
    public PlayerMelee MeleeState { get; private set; }
    public PlayerShot ShotState { get; private set; }
    public PlayerReload ReloadState { get; private set; }
    public PlayerDead DeadState { get; private set; }
    public PlayerInteract InteractState { get; private set; }
    public PlayerCall CallState { get; private set; }
    public PlayerDetantion DetantionState { get; private set; }
    public PlayerUseGadget UseGadgetState { get; private set; }
    public PlayerUseInventory UseInventoryState { get; private set; }
    #endregion

    #region Component
    public Rigidbody myRB { get; private set; }
    public CapsuleCollider myColl { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler inputController { get; private set; }
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

    private Plane plane = new Plane();
    private float distance = 0;

    public bool isHaveMainWeapon;
    public int nowHaveGadget = 0;

    [SerializeField]
    private PlayerInteractUI interactUI;
    [SerializeField]
    private PlayerInteractUI detantionUI;
    [SerializeField]
    private DamageEffect damageUI;
    [SerializeField]
    private PlayerHurtFlash hurtFlashUI;

    public PlayerInteractUI InteractUI { get; private set; }
    public PlayerInteractUI DetantionUI { get; private set; }

    public PlayerInventoryUI inventoryUI { get; private set; }
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
        InteractState = new PlayerInteract(this, stateMachine, playerData, "interact");
        CallState = new PlayerCall(this, stateMachine, playerData, "call");
        DetantionState = new PlayerDetantion(this, stateMachine, playerData, "detantion");
        UseGadgetState = new PlayerUseGadget(this, stateMachine, playerData, "useGadget");
        UseInventoryState = new PlayerUseInventory(this, stateMachine, playerData, "useInventory");
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myColl = GetComponent<CapsuleCollider>();
        if (InputManagerDontDestroy.Instance != null)
        {
            inputController = InputManagerDontDestroy.Instance.playerInputHandler;
            InputManagerDontDestroy.Instance.playerInput.SwitchCurrentActionMap(InputManagerDontDestroy.Instance.GetPlayerActionMapName());
        }
        else
            Debug.LogError("InputManagerが存在しません。");
        
        Anim = GetComponent<Animator>();
        Inventory = GetComponentInChildren<Inventory>();
        search = GetComponentInChildren<FunSearch>();
        inventoryUI = GetComponentInChildren<PlayerInventoryUI>();

        Inventory.SetMainWeapon();
        Inventory.SetGadget();
        inventoryUI.SetInventory(Inventory);

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
        gameManager.GameManager?.SetPlayerGun(gun);
        gameManager.GameManager?.SetPlayerDead(States.dead);
        stateMachine.Initialize(IdleState);
        States.SetInitHP(playerData.maxHP);
        InteractUI = interactUI;
        DetantionUI = detantionUI;
        InteractUI.Hide();
        detantionUI.Hide();

        hurtFlashUI.SetMaxHP(playerData.maxHP);
        hurtFlashUI.SetCurrentHP(playerData.maxHP);

        isHaveMainWeapon = true;
        inventoryUI.HideInventoryUI();
    }

    private void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();
        bool isClear = false;
        if(gameManager.GameManager != null)
            isClear = gameManager.GameManager.isGameClear;

        //�}�E�X�̈ʒu�����ʂɂȂ�悤�Ƀv���C���[����]�����鏈��
        if(!States.dead && !isClear)
        {
            if(stateMachine.CurrentState != UseGadgetState)
            {
                var ray = Camera.main.ScreenPointToRay(inputController.MousePosition);
                plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
                if(plane.Raycast(ray,out distance) && stateMachine.CurrentState != RunState)
                {
                    Vector3 lookpoint = ray.GetPoint(distance);
                    Rotation.SetRotation(lookpoint);
                }
            }
        }
        else if(isClear)
        {
            stateMachine.ChangeState(IdleState);
            stateMachine.canChangeState = false;
            Movement.CanSetVelocity = false;
            Rotation.CanSetRotate = false;
        }

        //Animator�ɕK�v�Ȓl�����鏈��
        AnimationInputValueSet();
        damageUI.LogicPlayerDamageUI(playerData.maxHP, States.currentHP);
        hurtFlashUI.SetCurrentHP(States.currentHP);
        
        //InventoryUIにマウスの座標をセット
        inventoryUI.SetMousePosition(inputController.MousePosition);

        //武器を持っていない時はメイン武器を非アクティブ状態に
        if (isHaveMainWeapon && !Inventory.GetWeaponActive())
            this.Inventory.SetWeaponActive(true);
        else if (!isHaveMainWeapon && Inventory.GetWeaponActive())
            this.Inventory.SetWeaponActive(false);
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
        Vector3 forward = this.gameObject.transform.forward;
        forward = new Vector3(forward.x, 0, forward.z).normalized;

        float inputForward = inputController.NormInputZ;
        float inputRight = inputController.NormInputX;
        if(inputForward == 1)
        {
            if(forward.x >= 0)
            {
                inputForward -= forward.x;
                inputRight -= forward.x;
            }
            else
            {
                inputForward -= Mathf.Abs(forward.x);
                inputRight += Mathf.Abs(forward.x);
            }
        }
        else if(inputForward == -1)
        {
            if (forward.x >= 0)
            {
                inputForward += forward.x;
                inputRight += forward.x;
            }
            else
            {
                inputForward += Mathf.Abs(forward.x);
                inputRight -= Mathf.Abs(forward.x);
            }
        }

        inputForward *= forward.z;
        inputRight *= forward.x;

        Anim.SetFloat("inputForward", inputForward);
        Anim.SetFloat("inputRight", inputRight);

        //Anim.SetFloat("playerDirectionX", this.gameObject.transform.forward.x);
        //Anim.SetFloat("playerDirectionZ", this.gameObject.transform.forward.z);
    }

    public bool CheckFrontObject(string tag, out GameObject gameObject, float distance)
    {
        RaycastHit hitObject;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        if (Physics.Raycast(pos, transform.forward, out hitObject, distance))
        {
            if(hitObject.transform.tag == tag)
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
