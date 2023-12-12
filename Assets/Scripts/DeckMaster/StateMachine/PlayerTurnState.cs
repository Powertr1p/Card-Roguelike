using System.Collections.Generic;
using Cards;
using DefaultNamespace.Player;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState(PlayerInput input, DeckSpawner spawner, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono) 
            : base(input, spawner, deckCards, playerCard, mono)
        {
            Name = TurnState.PlayerTurn;
            _deckCards = deckCards;
            _player = playerCard;
            _mono = mono;
            _input = input;
        }

        public override void Enter()
        {
            _input.EnableInput();
            base.Enter();
        }

        public override void Execute()
        {
            NextState = new DeckMasterState(_input, _spawner, _deckCards, _player, _mono);
            Stage = Event.Exit;
            
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}