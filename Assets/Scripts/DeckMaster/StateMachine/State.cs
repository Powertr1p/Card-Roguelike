using System;
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
        Debug.Log("ENTER");
        
        Stage = Event.Execute;
    }

    public virtual void Execute()
    {
        Debug.Log("EXEC");
        Stage = Event.Execute;
    }

    public virtual void Exit()
    {
        Debug.Log("EXIT");
        Stage = Event.Exit;
    }

    public State Process()
    {
        Debug.Log("PROCESS");
        
        if (Stage == Event.Enter)
            Enter();
        else if (Stage == Event.Execute)
            Execute();
        else if (Stage == Event.Exit)
        {
            Exit();
            return NextState;
        }

        return this;
    }
}
