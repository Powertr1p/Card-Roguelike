using System.Collections.Generic;
using Cards;
using DefaultNamespace.Player;
using UnityEditor.MPE;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState(PlayerInput input, DeckSpawner spawner, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono) : base(input, deckCards, playerCard, mono)
        {
            Name = TurnState.PlayerTurn;
        }

        public override void Enter()
        {
            Player.EventTurnEnded += ProcessState;
            
            Input.EnableInput();
            base.Enter();
        }

        public override void Execute()
        {
            NextState = new CardsAttackState(Input, DeckCards, Player, Mono);
            base.Execute();
        }

        public override void Exit()
        {
            Player.EventTurnEnded -= ProcessState;
            
            base.Exit();
        }

        private void ProcessState()
        {
            Process();
        }
    }
}