using DefaultNamespace.Interfaces;
using UnityEngine;

namespace Cards
{
    public class PlayerHeroCard : HeroCard, IDragAndDropable
    {
        [SerializeField] private DragAndDropObject _dragBehaviour;
        
        protected override void Consume(Card consumeCard)
        {
            
        }

        public void Grab()
        {
            _dragBehaviour.Grab();
        }

        public void PlaceInitialPosition()
        {
            _dragBehaviour.PlaceInitialPosition();
        }

        public void SetNewInitialPosition(Vector3 position)
        {
            _dragBehaviour.SetNewInitialPosition(position);
        }

        public void Drag()
        {
            _dragBehaviour.Drag();
        }

        public Vector3 GetPosition()
        {
            return _dragBehaviour.GetPosition();
        }
    }
}