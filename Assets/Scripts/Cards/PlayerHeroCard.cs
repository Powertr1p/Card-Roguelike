using DefaultNamespace.Interfaces;
using DefaultNamespace.Player;
using UnityEngine;

namespace Cards
{
    public class PlayerHeroCard : HeroCard, IDragAndDropable
    {
        [SerializeField] private DragAndDropObject _dragBehaviour;
        [SerializeField] private Raycaster _raycaster;
        
        public override void Interact(HeroCard interactorCard)
        {
        }

        public void StartDragState()
        {
            _dragBehaviour.EnterGrabState();
        }

        public void ExitDragState()
        {
            TryPlaceSelf();
            _dragBehaviour.ExitGrabState();
        }

        public void Drag()
        {
            _dragBehaviour.Drag();
        }

        private void TryPlaceSelf()
        {
            var hit = _raycaster.GetBoxcastNearestHit(transform);
            
            if (hit)
            {
                SetNewInitialPosition(hit.collider.transform.position);
                TryInteractWithOverlappedCard(hit);
            }
            else
            {
                PlaceInitialPosition();
            }
        }
        
        private void PlaceInitialPosition()
        {
            _dragBehaviour.PlaceInitialPosition();
        }

        private void SetNewInitialPosition(Vector3 position)
        {
            _dragBehaviour.SetNewInitialPosition(position);
        }

        private void TryInteractWithOverlappedCard(RaycastHit2D hit)
        {
            if (hit.collider.TryGetComponent(out EffectCard overlappedCard))
            {
                InteractWithOverlappedCard(overlappedCard);
            }
        }

        private void InteractWithOverlappedCard(Card card)
        {
            card.Interact(this);
        }
    }
}