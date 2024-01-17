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
            var openLimit = GameRulesGetter.Rules.VisibleZone;
            var position = Player.Data.Position;
            
            var cards =  GetCardsAroundPlayer(position - openLimit, position + openLimit, FaceSate.FaceUp);
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

                yield return new WaitForSeconds(GameRulesGetter.Rules.DelayBetweenCardsOpen);
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