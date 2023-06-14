using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerDontDestroy : MonoBehaviour
{
    public static InputManagerDontDestroy Instance;

    public PlayerInputHandler playerInputHandler { get; private set; }
    public GameEndInputHandler gameEndInputHandler { get; private set; }
    public PlayerInput playerInput { get; private set; }

    [SerializeField]
    private string PlayerActionMapName;
    [SerializeField]
    private string GameEndActionMapName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
            GameObject.Destroy(this.gameObject);

    }

    private void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        gameEndInputHandler = GetComponent<GameEndInputHandler>();
        playerInput = GetComponent<PlayerInput>();
    }

    public string GetPlayerActionMapName() { return PlayerActionMapName; }
    public string GetGameEndActionMapName() { return GameEndActionMapName; }
}
