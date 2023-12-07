using System;
using DefaultNamespace.Interfaces;
using DefaultNamespace.Player;
using UnityEngine;

namespace Cards
{
    public class PlayerHeroCard : HeroCard, IDragAndDropable
    {
        [SerializeField] private DragAndDropObject _dragBehaviour;
        [SerializeField] private Raycaster _raycaster;

        private bool _positioningTurn;
        
        public event Action<Vector2Int, Card> TurnEnded;

        private void Start()
        {
            _positioningTurn = true;
        }
        
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
                if (TryInteractWithOverlappedCard(hit))
                {
                    SetNewInitialPosition(hit.collider.transform.position);
                }
                else
                {
                    PlaceInitialPosition();
                }
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

        private bool TryInteractWithOverlappedCard(RaycastHit2D hit)
        {
            if (hit.collider.TryGetComponent(out EffectCard overlappedCard))
            {
                if (CanPlaceCard(overlappedCard.Data.Position))
                {
                    InteractWithOverlappedCard(overlappedCard);
                    return true;
                }

                return false;
            }

            return false;
        }

        private bool CanPlaceCard(Vector2Int desiredPosition)
        {
            //TODO: вынести все это отсюда
            
            if (_positioningTurn)
            {
                _positioningTurn = false;
                
                return desiredPosition.y == Data.Position.y - 1;
            }
            
            return (desiredPosition.x == Data.Position.x - 1 || desiredPosition.x == Data.Position.x + 1) && (desiredPosition.y == Data.Position.y - 1 || desiredPosition.y == Data.Position.y + 1);
        }

        private void InteractWithOverlappedCard(Card card)
        {
            card.Interact(this);
            Initialize(card.Data.Position);
            TurnEnded?.Invoke(this.Data.Position, card);
        }
    }
}