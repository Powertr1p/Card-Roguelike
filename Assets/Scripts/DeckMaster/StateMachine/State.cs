using System;
using System.Collections.Generic;
using Cards;
using DeckMaster;
using DefaultNamespace.Player;
using UnityEngine;
using Event = DeckMaster.Event;

public class State
{
    public TurnState Name;
    protected Event Stage;
    protected State NextState;
    protected PlayerInput _input;
    protected List<Card> _positionPlacements;
    protected List<DeckCard> _deckCards;
    protected DeckSpawner _spawner;
    protected PlayerHeroCard _player;
    protected MonoBehaviour _mono;

    public State(PlayerInput input, DeckSpawner spawner, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono)
    {
        Stage = Event.Enter;
        _input = input;
        _deckCards = deckCards;
        _spawner = spawner;
        _player = playerCard;
        _mono = mono;
    }

    public virtual void Enter()
    {
        Debug.Log( Name + " ENTER");
        
        Stage = Event.Execute;
    }

    public virtual void Execute()
    {
        Debug.Log(Name + " EXEC");
        Stage = Event.Execute;
    }

    public virtual void Exit()
    {
        Debug.Log(Name + " EXIT");
        Stage = Event.Exit;
    }

    public State Process()
    {
        Debug.Log(Name + " PROCESS");

        if (Stage == Event.Execute)
            Stage = Event.Exit;

        if (Stage == Event.Enter)
            Enter();
        if (Stage == Event.Execute)
            Execute();
        if (Stage == Event.Exit)
        {
            Exit();
            return NextState.Process();
        }

        return this;
    }
}
