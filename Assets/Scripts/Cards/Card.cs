using Data;
using UnityEngine;

namespace Cards
{
    public abstract class Card : MonoBehaviour
    {
        public CardPositionData PositionData => positionData;

        protected CardPositionData positionData;

        public virtual void InitializePosition(Vector2Int position)
        {
            positionData = new CardPositionData(position);
        }

        protected void SetPosition(Vector2Int position)
        {
            positionData = new CardPositionData(position);
        }

        public abstract void Interact(HeroCard heroCardConsumer);
    }
}