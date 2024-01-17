using Data;
using UnityEngine;

namespace Cards
{
    public abstract class Card : MonoBehaviour
    {
        public CardData Data => CardData;
        public LevelCardType Type => CardData.Type;
        public int Room => CardData.Room;
       
        protected CardData CardData;

        private bool _isInitialized;

        public virtual void Initialize(CardData data)
        {
            if (_isInitialized) return;
            _isInitialized = true;
            
            CardData = new CardData(data.Room, data.Type, data.Position);
        }

        public abstract void Interact(HeroCard heroCardConsumer);
    }
}