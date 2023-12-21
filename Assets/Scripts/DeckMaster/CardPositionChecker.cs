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
        
        public bool CanPositionCard(Vector2Int desirePosition, Vector2Int currentPosition)
        {
            if (desirePosition == currentPosition) return false;
            
            return (desirePosition.x >= currentPosition.x - _movingLimit && desirePosition.x <= currentPosition.x + _movingLimit) &&
                   (desirePosition.y >= currentPosition.y - _movingLimit && desirePosition.y <= currentPosition.y + _movingLimit);
        }
    }
}