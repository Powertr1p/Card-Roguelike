using System.Collections.Generic;
using Cards;
using DefaultNamespace.Player;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class PlayerPositioningState : State
    {
        public PlayerPositioningState(PlayerInput input, DeckSpawner spawner, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono) 
            : base(input, spawner, deckCards, playerCard, mono)
        {
            Name = TurnState.PlayerPositioningTurn;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            _positionPlacements = _spawner.SpawnPlacementsForPlayer();

            NextState = new DeckMasterState(_input, _spawner, _deckCards, _player, _mono);
            
            base.Execute();
        }

        public override void Exit()
        {
            foreach (var placement in _positionPlacements)
            {
                Object.Destroy(placement.gameObject);
            }
            
            base.Exit();
        }
    }
}