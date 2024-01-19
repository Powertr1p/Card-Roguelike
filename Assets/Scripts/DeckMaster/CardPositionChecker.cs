using Cards;
using Data;

namespace DeckMaster
{
    public class CardPositionChecker
    {
        private int _movingLimit;
        

        public CardPositionChecker(int movingLimit)
        {
            _movingLimit = movingLimit;
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
    }
}