using Cards;
using UnityEngine;

namespace Data
{
    public class CardData
    {
        public Vector2Int Position => _cardPositionData.Position;

        public int Room => _room;

        public LevelCardType Type => _type;
        
        private int _room = -1;
        private LevelCardType _type;
        private CardPositionData _cardPositionData;

        public CardData(int room, LevelCardType type, Vector2Int position)
        {
            _room = room;
            _type = type;
            _cardPositionData = new CardPositionData(position);
        }

        public CardData()
        {
            _room = -1;
            _type = LevelCardType.Unreachable;
        }

        public void SetNewPosition(Vector2Int position)
        {
            _cardPositionData = new CardPositionData(position);
        }
    }
}