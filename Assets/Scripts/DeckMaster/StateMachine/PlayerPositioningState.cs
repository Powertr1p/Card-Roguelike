using System.Collections.Generic;
using Cards;
using DefaultNamespace.Player;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class PlayerPositioningState : State
    {
        public PlayerPositioningState(PlayerInput input, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono, DeckSpawner spawner) : base(input, deckCards, playerCard, mono, spawner)
        {
            Name = TurnState.PlayerPositioningTurn;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            PositionPlacements = Spawner.SpawnPlacementsForPlayer();

            NextState = new OpenCardsState(Input, DeckCards, Player, Mono);
            
            base.Execute();
        }

        public override void Exit()
        {
            foreach (var placement in PositionPlacements)
            {
                Object.Destroy(placement.gameObject);
            }
            
            base.Exit();
        }
    }
}