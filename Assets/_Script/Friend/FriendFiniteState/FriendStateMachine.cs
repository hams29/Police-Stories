using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendStateMachine
{
    public bool canChangeState;
    public FriendState CurrentState { get; private set; }
    public FriendState OldState { get; private set; }

    public void Initialize(FriendState iniState)
    {
        canChangeState = true;
        CurrentState = iniState;
        CurrentState.Enter();
        OldState = CurrentState;
    }

    public void ChangeState(FriendState nextState)
    {
        if (!canChangeState)
            return;

        CurrentState.Exit();
        OldState = CurrentState;
        CurrentState = nextState;
        CurrentState.Enter();
    }
}
