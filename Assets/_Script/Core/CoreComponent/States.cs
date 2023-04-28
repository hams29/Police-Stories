using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : CoreComponent
{
    public float currentHP { get; private set; }
    public bool dead { get; private set; }

    private float maxHP;

    protected override void Awake()
    {
        base.Awake();
        currentHP = 0;
        dead = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (currentHP <= 0)
            dead = true;
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

    #endregion
}
