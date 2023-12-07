using Data;
using UnityEngine;

namespace Cards
{
    public abstract class Card : MonoBehaviour
    {
        public CardData Data => _data;

        protected CardData _data;

        public virtual void Initialize(Vector2Int position)
        {
            _data = new CardData(position);
        }

        public abstract void Interact(HeroCard heroCardConsumer);
    }
}