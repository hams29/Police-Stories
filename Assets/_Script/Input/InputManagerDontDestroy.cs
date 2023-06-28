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
        if (this.playerInputHandler == null)
            playerInputHandler = GetComponent<PlayerInputHandler>();

        gameEndInputHandler = GetComponent<GameEndInputHandler>();

        if (this.playerInput == null)
            playerInput = GetComponent<PlayerInput>();
    }

    public string GetPlayerActionMapName() { return PlayerActionMapName; }
    public string GetGameEndActionMapName() { return GameEndActionMapName; }

    public PlayerInputHandler GetPlayerInputHandler()
    {
        if(this.playerInputHandler == null)
        {
            this.playerInputHandler = GetComponent<PlayerInputHandler>();
        }

        return this.playerInputHandler;
    }

    public void SetPlayerInputActionMap(string actionMapName)
    {
        if(this.playerInput == null)
            this.playerInput = GetComponent<PlayerInput>();

        playerInput.SwitchCurrentActionMap(actionMapName);
    }

    public GameEndInputHandler GetGameEndUIInputHandler(ref GameEndInputHandler ret)
    {
        ret = gameEndInputHandler;
        return gameEndInputHandler;
    }
}
