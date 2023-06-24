using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.VersionControl.Asset;

public class DoorOpen : MonoBehaviour
{
     [SerializeField] Animator doorAC;
    [SerializeField] private GameObject NavMeshObs;
    Core core;
    private Interact interact;
    private Interact Interact { get => interact ?? core.GetCoreComponent(ref interact); }

    public bool isOpened { get; private set; } = false;
    bool useInteract = false;

    // Start is called before the first frame update
    private void Awake()
    {
        doorAC = GetComponent<Animator>();
    }

    private void Start()
    {
        core = GetComponentInChildren<Core>();
        Interact.canInteract = true;
        NavMeshObs.SetActive(false);
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
            NavMeshObs.SetActive(true);
        }
        else if (Interact.isInteract && isOpened && !useInteract)
        {
            isOpened = false;
            doorAC.SetBool("opened", isOpened);
            doorAC.SetBool("closed", !isOpened);
            useInteract = true;
            NavMeshObs.SetActive(false);
        }
    }

    public void SetAnimTriggger()
    {
        Interact.UseInteract();
        useInteract = false;
    }

}
