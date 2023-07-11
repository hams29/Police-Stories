using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState CurrentState { get; private set; }
    public EnemyState OldState { get; private set; }

    private bool canChangeState;

    public void Initialize(EnemyState iniState)
    {
        CurrentState = iniState;
        CurrentState.Enter();
        OldState = CurrentState;
        canChangeState = true;
    }

    public void ChangeState(EnemyState nextState)
    {
        if(canChangeState)
        {
            OldState = CurrentState;
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }
    }

    public void SetCanChangeState(bool flg) => canChangeState = flg;
}
