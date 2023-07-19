using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : CoreComponent, ILogicUpdate
{
    public float currentHP { get; private set; }
    public bool dead { get; private set; }

    private float maxHP;

    private WeakeningState nowWeakening = WeakeningState.None;
    private float weakeningStartTime;
    private float weakeningHoldTime;

    public enum WeakeningState
    {
        None,
        FrashBang
    }

    protected override void Awake()
    {
        base.Awake();
        currentHP = 0;
        dead = false;
        nowWeakening = WeakeningState.None;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (currentHP <= 0)
            dead = true;

        switch(nowWeakening)
        {
            case WeakeningState.FrashBang:
                if(weakeningStartTime + weakeningHoldTime < Time.time)
                {
                    nowWeakening = WeakeningState.None;
                }
                break;
        }
    }

    #region Set Function
    public void SetInitHP(float hp)
    {
        maxHP = hp;
        currentHP = hp;
    }

    public void addDamage(float addDamage)
    {
        if (currentHP - addDamage <= 0)
            currentHP = 0;
        else
            currentHP -= addDamage;
    }

    public void addHealth(float addHealth)
    {
        if (currentHP + addHealth >= maxHP)
            currentHP = maxHP;
        else
            currentHP += addHealth;
    }

    public void SetWeakeningState(WeakeningState weake,float holdTime)
    {
        nowWeakening = weake;
        weakeningStartTime = Time.time;
        weakeningHoldTime = holdTime;
    }
    #endregion
}
