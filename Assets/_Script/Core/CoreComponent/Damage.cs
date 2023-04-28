using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : CoreComponent, ILogicUpdate
{
    private States States { get => states ?? core.GetCoreComponent(ref states); }
    private States states;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    #region Set Function
    public void addDamage(float damage)
    {
        States.addDamage(damage);
    }
    #endregion
}
