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
        SetActualState();
        //Debug.Log(Name + " PROCESS");

          if (Stage == Event.Execute)
               Stage = Event.Exit;

        if (Stage == Event.Enter)
            Enter();
        if (Stage == Event.Execute)
            Execute();
        else if (Stage == Event.Exit)
        {
            Exit();
            return NextState.Process();
        }
        
        return this;
    }

    private void SetActualState()
    {
        GameStateGetter.UpdateState(this);
    }

    protected List<DeckCard> GetCardsAroundPlayer(Vector2Int startPosition, Vector2Int endPosition, FaceSate skipCondition)
    {
        var pickedCards = new List<DeckCard>();
        var position = Player.PositionData.Position;

        foreach (var card in DeckCards)
        {
            if (card.Facing == skipCondition || card.Condition == CardCondition.Dead) continue;
            if (card.Room != Player.Room) continue;

            if (card.PositionData.Position.y >= startPosition.y && card.PositionData.Position.y <= endPosition.y)
            {
                if (card.PositionData.Position.x >= startPosition.x && card.PositionData.Position.x <= endPosition.x)
                {
                    pickedCards.Add(card);
                }
            }
        }

        return pickedCards;
    }
}
