using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState CurrentState { get; private set; }
    public EnemyState OldState { get; private set; }

    public void Initialize(EnemyState iniState)
    {
        CurrentState = iniState;
        CurrentState.Enter();
        OldState = CurrentState;
    }

    public void ChangeState(EnemyState nextState)
    {
        OldState = CurrentState;
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
}
