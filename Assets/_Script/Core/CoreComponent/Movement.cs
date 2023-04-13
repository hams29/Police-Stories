using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent, ILogicUpdate
{
    public Rigidbody myRB { get; private set; }
    public bool CanSetVelocity { get; set; }
    public Vector3 CurrentVelocity { get; private set; }

    private Vector3 worlspace;

    protected override void Awake()
    {
        base.Awake();
        myRB = GetComponentInParent<Rigidbody>();

        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = myRB.velocity;
    }

    #region Set Function
    public void SetVelocityZero()
    {
        worlspace = Vector3.zero;
        SetFinalVelocity();        
    }

    //TODO::Movement::‘±‚«

    private void SetFinalVelocity()
    {
        if(CanSetVelocity)
        {
            myRB.velocity = worlspace;
            CurrentVelocity = worlspace;
        }
    }

    #endregion
}
