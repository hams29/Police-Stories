using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector3 RawMovementInput { get; private set; }

    public Vector2 MousePosition { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputZ { get; private set; }
    
    public bool DashInput { get; private set; }
    public bool ReloadInput { get; private set; }
    public bool ReloadInputStop { get; private set; }
    public bool MeleeInput { get; private set; }
    public bool MeleeInputStop { get; private set; }
    public bool InteractInputStop { get; private set; }
    public bool ShotInput { get; private set; }
    public bool InteractInput { get; private set; }

    private float reloadInputStartTime;
    private float meleeInputStartTIme;
    private float interactInputStartTime;

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
    }

    private void Update()
    {
        CheckReloadInputHoldTime();
        CheckMeleeInputHoldTime();
        CheckInteractInputHoldTime();
    }
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector3>();

        //Debug.Log(RawMovementInput);

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputZ = Mathf.RoundToInt(RawMovementInput.z);
    }

    public void OnReloadInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ReloadInput = true;
            reloadInputStartTime = Time.time;
            ReloadInputStop = false;
        }

        if(context.started)
        {
            ReloadInputStop = true;
        }
    }

    public void OnMeleeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            MeleeInput = true;
            meleeInputStartTIme = Time.time;
            MeleeInputStop = false;
        }

        if(context.canceled)
        {
            MeleeInputStop = true;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
            DashInput = true;

        if (context.canceled)
            DashInput = false;
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void OnShotInput(InputAction.CallbackContext context)
    {
        if(context.started)
            ShotInput = true;

        if (context.canceled)
            ShotInput = false;
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            InteractInput = true;
            interactInputStartTime = Time.time;
            InteractInputStop = false;
        }

        if(context.canceled)
        {
            InteractInputStop = true;
        }
    }

    public void UseReloadInput() => ReloadInput = false;
    public void UseMeleeInput() => MeleeInput = false;
    public void UseShotInput() => ShotInput = false;
    public void UseInteractInput() => InteractInput = false;

    private void CheckReloadInputHoldTime()
    {
        if(Time.time >= reloadInputStartTime + inputHoldTime)
            ReloadInput = false;
    }

    private void CheckMeleeInputHoldTime()
    {
        if (Time.time >= meleeInputStartTIme + inputHoldTime)
            MeleeInput = false;
    }

    private void CheckInteractInputHoldTime()
    {
        if (Time.time > interactInputStartTime + inputHoldTime)
            InteractInput = false;
    }
}
