using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : CoreComponent
{
    public bool isInteract;
    protected override void Awake()
    {
        base.Awake();
        isInteract = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Debug.Log(transform.root.name + " interact " + isInteract);
    }

    #region SetFunction
    public void SetInteract() => isInteract = true;
    public void UseInteract() => isInteract = false;
    #endregion
}
