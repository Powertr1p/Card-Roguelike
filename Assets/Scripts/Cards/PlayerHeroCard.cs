using System;
using DeckMaster;
using DefaultNamespace.Interfaces;
using Player;
using UnityEngine;

namespace Cards
{
    public class PlayerHeroCard : HeroCard, IDragAndDropable
    {
        [SerializeField] private DragAndDropObject _dragBehaviour;
        [SerializeField] private Raycaster _raycaster;

        public event Action EventTurnEnded;
        public event Action<Vector2Int> EventPlacing;

        private CardPositionChecker _positionChecker;
        private readonly Vector2Int _initialPosition = new Vector2Int(-2, -2);

        protected override void Awake()
        {
            base.Awake();
            
            _positionChecker = new CardPositionChecker();
        }

        private void Start()
        {
            SetPosition(_initialPosition);
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
            if (hit.collider.TryGetComponent(out Card overlappedCard))
            {
                EventPlacing?.Invoke(overlappedCard.PositionData.Position);
                
                if (CanPlaceCard(overlappedCard.PositionData.Position))
                {
                    InteractWithOverlappedCard(overlappedCard);
                    return true;
                }
                
                return false;
            }

            return false;
        }

        private bool CanPlaceCard(Vector2Int desirePosition)
        {
            if (GameStateGetter.State == TurnState.PlayerPositioningTurn)
            {
                return desirePosition.y == -1;
            }

            return _positionChecker.CanPositionCard(desirePosition, PositionData.Position);
        }

        private void InteractWithOverlappedCard(Card card)
        {
            if (card.TryGetComponent(out DeckCard deckCard))
            {
                if (deckCard.Condition == CardCondition.Alive)
                    card.Interact(this);
            }
            
            SetPosition(card.PositionData.Position);
            EventTurnEnded?.Invoke();
        }
    }
}