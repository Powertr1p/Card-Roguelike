using Cards;

namespace Data
{
    public class CardData
    {
        public CardPositionData CardPositionData;

        public int Room => _room;

        public LevelCardType Type => _type;
        
        private int _room = -1;
        private LevelCardType _type;

        public CardData(int room, LevelCardType type)
        {
            _room = room;
            _type = type;
        }

        public CardData()
        {
            _room = -1;
            _type = LevelCardType.Unreachable;
        }
    }
}