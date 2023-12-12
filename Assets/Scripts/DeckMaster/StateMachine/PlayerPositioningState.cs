using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class PlayerPositioningState : State
    {
        private List<Card> _positionPlacements;
        private List<DeckCard> _deckCards;
        private DeckSpawner _spawner;
        private PlayerHeroCard _player;
        private MonoBehaviour _mono;

        public PlayerPositioningState(DeckSpawner spawner, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono)
        {
            Name = TurnState.PlayerPositioningTurn;
            _deckCards = deckCards;
            _spawner = spawner;
            _player = playerCard;
        }

        public override void Enter()
        {
            _positionPlacements = _spawner.SpawnPlacementsForPlayer();
            
            base.Enter();
        }

        public override void Execute()
        {
            foreach (var placement in _positionPlacements)
            {
                Object.Destroy(placement.gameObject);
            }
            
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