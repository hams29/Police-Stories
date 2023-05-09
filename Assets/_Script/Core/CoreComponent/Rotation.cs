using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : CoreComponent,ILogicUpdate
{
    public bool CanSetRotate { get; set; }
    public Vector3 CurrentRotate { get; private set; }

    private Transform myTran;

    private Vector3 workspace;

    protected override void Awake()
    {
        base.Awake();
        myTran = transform.parent.parent;
        CanSetRotate = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    #region Set Function
    public void SetRotation(Vector3 look)
    {
        workspace = look;
        SetFinalRotation();
    }

    private void SetFinalRotation()
    {
        if(CanSetRotate)
        {
            myTran.LookAt(workspace);
            CurrentRotate = workspace;
        }
    }
    #endregion
}
