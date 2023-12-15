using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using DefaultNamespace.Player;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class OpenCardsState : State
    {
        private readonly Vector2Int _visibleZone = new Vector2Int(2,2);
        
        public OpenCardsState(PlayerInput input, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono) : base(input, deckCards, playerCard, mono)
        {
            Name = TurnState.OpenCardsState;
        }

        public override void Enter()
        {
            Input.DisableInput();
            base.Enter();
        }

        public override void Execute()
        {
            var cards =  GetCardsAroundPlayer(Player.Data.Position - _visibleZone, Player.Data.Position + _visibleZone, FaceSate.FaceUp);
            Mono.StartCoroutine(OpenCards(cards, MoveToNextState));
        }

        public override void Exit()
        {
            base.Exit();
        }

        private IEnumerator OpenCards(List<DeckCard> cardsToOpen, Action completeCallback)
        {
            foreach (var card in cardsToOpen)
            {
                card.OpenCard();

                yield return new WaitForSeconds(0.1f);
            }
            
            completeCallback?.Invoke();
        }

        private void MoveToNextState()
        {
            NextState = new PlayerTurnState(Input, Spawner, DeckCards, Player, Mono);
            base.Execute();
            Process();
        }
    }
}