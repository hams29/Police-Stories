using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenStateMachine
{
    public CitizenState CurrentState { get; private set; }
    public CitizenState OldState { get; private set; }

    public void Initialize(CitizenState initState)
    {
        CurrentState = initState;
        CurrentState.Enter();
        OldState = CurrentState;
    }

    public void ChangeState(CitizenState nextState)
    {
        OldState = CurrentState;
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
}
