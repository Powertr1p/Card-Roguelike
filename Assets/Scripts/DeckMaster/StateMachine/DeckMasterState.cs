using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace DeckMaster.StateMachine
{
    public class DeckMasterState : State
    {
        private List<DeckCard> _deckCards;
        private PlayerHeroCard _player;
        private MonoBehaviour _mono;

        private Vector2Int _visibleZone = new Vector2Int(2,2);
        
        public DeckMasterState(List<DeckCard> deckCards, PlayerHeroCard playerCard, MonoBehaviour mono)
        {
            Name = TurnState.DeckMasterTurn;
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
            OpenCardsState();
            
            NextState = new PlayerTurnState(_deckCards, _player, _mono);
            Stage = Event.Exit;
            
            base.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private void OpenCardsState()
        {
            var cards =  GetCardsAroundPlayer(_player.Data.Position - _visibleZone, _player.Data.Position + _visibleZone, FaceSate.FaceUp);
            _mono.StartCoroutine(OpenCards(cards));
        }
        
        private List<DeckCard> GetCardsAroundPlayer(Vector2Int startPosition, Vector2Int endPosition, FaceSate skipCondition)
        {
            var pickedCards = new List<DeckCard>();

            foreach (var card in _deckCards)
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

                yield return new WaitForSeconds(0.1f);;
            }
        }
    }
}