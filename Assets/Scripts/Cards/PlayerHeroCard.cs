using System;
using DeckMaster;
using DefaultNamespace.Interfaces;
using DefaultNamespace.Player;
using Player;
using UnityEngine;

namespace Cards
{
    public class PlayerHeroCard : HeroCard, IDragAndDropable
    {
        [SerializeField] private DragAndDropObject _dragBehaviour;
        [SerializeField] private Raycaster _raycaster;

        private bool _positioningTurn;
        
        public event Action<Vector2Int, Card> EventTurnEnded;
        public event Action<Vector2Int> EventPlacing;

        private CardPositionChecker _positionChecker;

        protected override void Awake()
        {
            base.Awake();
            
            _positionChecker = new CardPositionChecker();
        }
        
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
            if (hit.collider.TryGetComponent(out DeckCard overlappedCard))
            {
                var type = overlappedCard.GetType();
                
                EventPlacing?.Invoke(overlappedCard.Data.Position);
                
                if (CanPlaceCard(overlappedCard.Data.Position))
                {
                    _positioningTurn = false;
                    InteractWithOverlappedCard(overlappedCard);
                    return true;
                }

                return false;
            }

            return false;
        }

        private bool CanPlaceCard(Vector2Int desirePosition)
        {
            if (_positioningTurn)
            {
                return desirePosition.y == Data.Position.y - 1;
            }

            return _positionChecker.CanPositionCard(desirePosition, Data.Position);
        }

        private void InteractWithOverlappedCard(Card card)
        {
            if (card.TryGetComponent(out EnemyCard enemy))
            {
                
            }
            else
            {
                
            }
            
            card.Interact(this);
            Initialize(card.Data.Position);
            EventTurnEnded?.Invoke(this.Data.Position, card);
        }
    }
}