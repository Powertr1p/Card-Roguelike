using Data;
using UnityEngine;

namespace Cards
{
    public abstract class Card : MonoBehaviour
    {
        public CardPositionData PositionData => CardData.CardPositionData;
        protected CardData CardData;

        private bool _isInitialized;

        public void Initialize(CardData data, Vector2Int position)
        {
            if (_isInitialized) return;
            _isInitialized = true;
            
            CardData = data;
            
            SetPosition(position);
        }
        
        public virtual void SetPosition(Vector2Int position)
        {
            CardData ??= new CardData();

            CardData.CardPositionData = new CardPositionData(position);
        }
        
        public abstract void Interact(HeroCard heroCardConsumer);
    }
}