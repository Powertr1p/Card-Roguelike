using System.Collections;
using System.Collections.Generic;
using DeckMaster;
using UnityEngine;
using Event = DeckMaster.Event;

public class State
{
    public TurnState Name;
    protected Event Stage;
    protected State NextState;

    public State()
    {
        Stage = Event.Enter;
    }

    public virtual void Enter()
    {
        Stage = Event.Execute;
    }

    public virtual void Execute()
    {
        Stage = Event.Execute;
    }

    public virtual void Exit()
    {
        Stage = Event.Exit;
    }

    public State Process()
    {
        if (Stage == Event.Enter)
            Enter();
        if (Stage == Event.Execute)
            Execute();
        if (Stage == Event.Exit)
        {
            Exit();
            return NextState;
        }

        return this;
    }
}
