using Cards;
using UnityEngine;

namespace DeckMaster
{
    public class CardPositionChecker
    {
        private int _movingLimit;

        public CardPositionChecker(int movingLimit)
        {
            _movingLimit = movingLimit;
        }
        
        public bool CanPositionCard(Card card, Vector2Int currentPosition)
        {
            var desirePosition = card.PositionData.Position;
            
            if (desirePosition == currentPosition) return false;
            if (card.Type == LevelCardType.Block) return false;

            return (desirePosition.x >= currentPosition.x - _movingLimit && desirePosition.x <= currentPosition.x + _movingLimit) &&
                   (desirePosition.y >= currentPosition.y - _movingLimit && desirePosition.y <= currentPosition.y + _movingLimit);
        }
    }
}