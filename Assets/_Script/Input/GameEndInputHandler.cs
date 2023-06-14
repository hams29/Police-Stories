using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEndInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    [SerializeField]
    private float inputHoldTime = 0.2f;

    public bool RestartInput { get; private set; }
    public bool TransSelectStageInput{ get; private set; }
    public bool RestartInputCanceled { get; private set; }
    public bool TransSelectStageInputCanceled { get; private set; }

    private float restartInputStartTime;
    private float transSelectStageInputStartTime;

    private void Start()
    {
        playerInput = InputManagerDontDestroy.Instance.playerInput;
        cam = Camera.main;
    }

    private void Update()
    {
        CheckRestartInput();
        CheckTransSelectStageInput();
    }

    public void OnRestartInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            RestartInput = true;
            restartInputStartTime = Time.time;
            RestartInputCanceled = false;
        }

        if(context.canceled)
        {
            RestartInputCanceled = true;
        }
    }

    public void OnTransSelectStageInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            TransSelectStageInput = true;
            transSelectStageInputStartTime = Time.time;
            TransSelectStageInputCanceled = false;
        }

        if(context.canceled)
        {
            TransSelectStageInputCanceled = true;
        }
    }

    private void CheckRestartInput()
    {
        if(RestartInput)
        {
            if (Time.time > restartInputStartTime + inputHoldTime)
                RestartInput = false;
        }
    }

    private void CheckTransSelectStageInput()
    {
        if(TransSelectStageInput)
        {
            if (Time.time > transSelectStageInputStartTime + inputHoldTime)
                TransSelectStageInput = false;
        }
    }

    public void UseRestartInput() => RestartInput = false;
    public void UseTransSelectStageInput() => TransSelectStageInput = false;
}
