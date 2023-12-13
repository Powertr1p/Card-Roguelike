using System.Collections;
using System.Collections.Generic;
using Cards;
using DefaultNamespace.Player;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class DeckMasterState : State
    {
        private readonly Vector2Int _visibleZone = new Vector2Int(2,2);
        
        public DeckMasterState(PlayerInput input, List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono) : base(input, deckCards, playerCard, mono)
        {
            Name = TurnState.DeckMasterTurn;
        }

        public override void Enter()
        {
            Input.DisableInput();
            base.Enter();
        }

        public override void Execute()
        {
            OpenCardsState();
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private void OpenCardsState()
        {
            var cards =  GetCardsAroundPlayer(Player.Data.Position - _visibleZone, Player.Data.Position + _visibleZone, FaceSate.FaceUp);
            Mono.StartCoroutine(OpenCards(cards));
        }
        
        private List<DeckCard> GetCardsAroundPlayer(Vector2Int startPosition, Vector2Int endPosition, FaceSate skipCondition)
        {
            var pickedCards = new List<DeckCard>();

            foreach (var card in DeckCards)
            {
                if (card.Facing == skipCondition || card.Condition == CardCondition.Dead) continue;

                if (card.Data.Position.y >= startPosition.y && card.Data.Position.y <= endPosition.y)
                {
                    if (card.Data.Position.x >= startPosition.x && card.Data.Position.x <= endPosition.x)
                    {
                        pickedCards.Add(card);
                    }
                }
            }

            return pickedCards;
        }
        
        private IEnumerator OpenCards(List<DeckCard> cardsToOpen)
        {
            foreach (var card in cardsToOpen)
            {
                card.OpenCard();

                yield return new WaitForSeconds(0.1f);
            }
            
            NextState = new PlayerTurnState(Input, Spawner, DeckCards, Player, Mono).Process();
        }
    }
}