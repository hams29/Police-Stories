using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendStateMachine
{
    public bool canChangeState;
    public FriendState CurrentState { get; private set; }

    public void Initialize(FriendState iniState)
    {
        canChangeState = true;
        CurrentState = iniState;
        CurrentState.Enter();
    }

    public void ChangeState(FriendState nextState)
    {
        if (!canChangeState)
            return;

        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
}
