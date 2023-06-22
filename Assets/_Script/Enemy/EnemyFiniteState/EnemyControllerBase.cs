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
    public PlayerSearch PlayerSearch { get;protected set; }

    protected States States { get => states ?? Core.GetCoreComponent(ref states); }
    private States states;

    protected Show Show { get => show ?? Core.GetCoreComponent(ref show); }
    private Show show;


    #endregion

    #region Variables
    public float playerOutOfViewTime { get; private set; }
    public bool isPlayerOutOfView { get; private set; }
    private List<Renderer> renderers = new List<Renderer>();
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
        PlayerSearch = GetComponentInChildren<PlayerSearch>();
        playerOutOfViewTime = 0.0f;
        isPlayerOutOfView = true;

        if (gameManager.GameManager != null)
            gameManager.GameManager.addMaxEnemy();

        Renderer[] material = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < material.Length; i++)
            renderers.Add(material[i]);

        Show?.InitMaterials(renderers);
    }

    protected virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.CurrentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {

    }

    public virtual void PlayerCall(Vector3 ppos)
    {

    }

    public void PlayerOutOfViewTIme() => playerOutOfViewTime = Time.time;
    public void SetPlayerOutOfView(bool flg) => isPlayerOutOfView = flg;
}
