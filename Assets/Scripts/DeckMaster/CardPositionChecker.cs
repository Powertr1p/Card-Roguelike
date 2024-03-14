using System.Collections.Generic;
using Cards;
using Data;
using UnityEngine;

namespace DeckMaster
{
    public class CardPositionChecker
    {
        private int _movingLimit;
        private List<DeckCard> _deckCards;
        private PlayerHeroCard _player;
        
        public CardPositionChecker(int movingLimit)
        {
            _movingLimit = movingLimit;
        }

        public CardPositionChecker(List<DeckCard> deckCards, PlayerHeroCard player)
        {
            _deckCards = deckCards;
            _player = player;
        }
        
        public bool CanPositionCard(Card targetCard, CardData playerCard)
        {
            var desirePosition = targetCard.Data.Position;
            var currentPosition = playerCard.Position;
            
            if (desirePosition == currentPosition) return false;
            if (targetCard.Type == LevelCardType.Block) return false;
            if (targetCard.Room > playerCard.Room && playerCard.Type != LevelCardType.Door) return false;

            return (desirePosition.x >= currentPosition.x - _movingLimit && desirePosition.x <= currentPosition.x + _movingLimit) &&
                   (desirePosition.y >= currentPosition.y - _movingLimit && desirePosition.y <= currentPosition.y + _movingLimit);
        }

        public bool IsAnyCardAroundPlayer()
        {
            if (!GameRulesGetter.Rules.IsBackTracking)
            {
                var movingLimit = new Vector2Int(GameRulesGetter.Rules.PlayerMovingLimit, GameRulesGetter.Rules.PlayerMovingLimit);
                var position = _player.Data.Position;
                
                var cards = GetCardsAroundPlayer(position - movingLimit, position + movingLimit);

                int cardsToMove = 0;

                foreach (var card in cards)
                {
                    if (card.Type == LevelCardType.Block) continue;
                    
                    cardsToMove++;
                }

                return cardsToMove != 0;
            }

            return true;
        }
        
        private List<DeckCard> GetCardsAroundPlayer(Vector2Int startPosition, Vector2Int endPosition)
        {
            var pickedCards = new List<DeckCard>();

            foreach (var card in _deckCards)
            {
                if (card.Condition == CardCondition.Dead) continue;

                if (_player.Data.Type != LevelCardType.Door)
                {
                    if (card.Data.Room != _player.Data.Room) continue;
                }
                
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
    }
}