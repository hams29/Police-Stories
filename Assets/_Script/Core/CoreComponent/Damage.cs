using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : CoreComponent, ILogicUpdate
{
    private States States { get => states ?? core.GetCoreComponent(ref states); }
    private States states;
    public bool isDamage { get; private set; }
    public bool isMeleeDamage { get; private set; }
    public Vector3 shotAnyPos { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        isDamage = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    #region Set Function
    public void addDamage(float damage)
    {
        States?.addDamage(damage);
        isDamage = true;
        //Debug.Log(transform.root.gameObject.name + " is " + damage + "damage");
    }

    public void addMeleeDamage(float damage)
    {
        States?.addDamage(damage);
        isDamage = true;
        isMeleeDamage = true;
    }

    public void UseDamageHandler() { isDamage = false; }
    public void UseMeleeDamageHundler() { isMeleeDamage = false; }
    public void SetShotAnyPos(Vector3 pos) { shotAnyPos = pos; }
    public void SetMeleeAnyPos(Vector3 pos) { shotAnyPos = pos; }
    #endregion
}
