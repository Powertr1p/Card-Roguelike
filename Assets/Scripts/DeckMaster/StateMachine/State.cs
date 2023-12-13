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
    protected readonly PlayerInput Input;
    protected List<Card> PositionPlacements;
    protected readonly List<DeckCard> DeckCards;
    protected readonly DeckSpawner Spawner;
    protected readonly PlayerHeroCard Player;
    protected readonly MonoBehaviour Mono;

    public State(PlayerInput input, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono, DeckSpawner spawner = null)
    {
        Stage = Event.Enter;
        Input = input;
        DeckCards = deckCards;
        Spawner = spawner;
        Player = playerCard;
        Mono = mono;
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
