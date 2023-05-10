using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class DoorOpen : MonoBehaviour
{
     [SerializeField] Animator doorAC;
    Core core;
    private Interact interact;
    private Interact Interact { get => interact ?? core.GetCoreComponent(ref interact); }

    bool isOpened = false;
    bool useInteract = false;

    // Start is called before the first frame update
    private void Awake()
    {
        doorAC = GetComponent<Animator>();
    }

    private void Start()
    {
        core = GetComponentInChildren<Core>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Interact.isInteract && !isOpened && !useInteract)
        {
            isOpened = true;
            doorAC.SetBool("opened",isOpened);
            doorAC.SetBool("closed", !isOpened);
            useInteract = true;
        }
        else if (Interact.isInteract && isOpened && !useInteract)
        {
            isOpened = false;
            doorAC.SetBool("opened", isOpened);
            doorAC.SetBool("closed", !isOpened);
            useInteract = true;
        }

    }

    public void SetAnimTriggger()
    {
        Interact.UseInteract();
        useInteract = false;
    }

}
