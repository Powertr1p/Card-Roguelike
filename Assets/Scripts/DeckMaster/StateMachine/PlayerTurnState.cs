using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class PlayerTurnState : State
    {
        private List<DeckCard> _deckCards;
        private PlayerHeroCard _player;
        private MonoBehaviour _mono;

        public PlayerTurnState(List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono)
        {
            Name = TurnState.PlayerTurn;
            _deckCards = deckCards;
            _player = playerCard;
            _mono = mono;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            NextState = new DeckMasterState(_deckCards, _player, _mono);
            Stage = Event.Exit;
            
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}