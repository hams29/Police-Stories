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

    public bool CanvasChangeNextInput { get; private set; }
    public bool CanvasChangeNextInputCanceled { get; private set; }
    
    public bool CanvasChangeReturnInput { get; private set; }
    public bool CanvasChangeReturnInputCanceled { get; private set; }



    private float restartInputStartTime;
    private float transSelectStageInputStartTime;
    private float canvasChangeInputTime;
    private float canvasChangeReturnInputTime;

    private void Start()
    {
        playerInput = InputManagerDontDestroy.Instance.playerInput;
        cam = Camera.main;
    }

    private void Update()
    {
        CheckRestartInput();
        CheckTransSelectStageInput();
        CheckNextInput();
        CheckReturnInput();
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

    public void OnNextInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CanvasChangeNextInput = true;
            canvasChangeInputTime = Time.time;
            CanvasChangeNextInputCanceled = false;
        }

        if (context.canceled)
        {
            CanvasChangeNextInputCanceled = true;
        }
    }

    public void OnReturnInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CanvasChangeReturnInput = true;
            canvasChangeReturnInputTime = Time.time;
            CanvasChangeReturnInputCanceled = false;
        }

        if (context.canceled)
        {
            CanvasChangeReturnInputCanceled = true;
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

    private void CheckNextInput()
    {
        if (CanvasChangeNextInput)
        {
            if (Time.time > canvasChangeInputTime + inputHoldTime)
                CanvasChangeNextInput = false;
        }
    }
    private void CheckReturnInput()
    {
        if (CanvasChangeReturnInput)
        {
            if (Time.time > canvasChangeReturnInputTime + inputHoldTime)
                CanvasChangeReturnInput = false;
        }
    }


    public void UseRestartInput() => RestartInput = false;
    public void UseTransSelectStageInput() => TransSelectStageInput = false;
    public void UseNextInput() => CanvasChangeNextInput = false;
    public void UseCheckReturnInput() => CanvasChangeReturnInput = false;
}
