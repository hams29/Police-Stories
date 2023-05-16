using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : CoreComponent
{
    public bool isInteract;
    public bool canInteract;
    protected override void Awake()
    {
        base.Awake();
        isInteract = false;
        canInteract = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Debug.Log(transform.root.name + " interact " + isInteract);
        //Debug.Log(transform.root.name + " canInteract " + canInteract);
    }

    #region SetFunction
    public void SetInteract() => isInteract = true;
    public void UseInteract() => isInteract = false;
    #endregion
}
