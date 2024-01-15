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

        private DeckCard _lastDragHit;

        protected override void Awake()
        {
            base.Awake();
            
            _positionChecker = new CardPositionChecker(GameRulesGetter.Rules.PlayerMovingLimit);
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
            TrySelectHoverCard();
        }

        private void TrySelectHoverCard()
        {
            var hit = _raycaster.GetBoxcastNearestHit(transform);

            DeselectLastDraggedCard();

            if (hit && hit.collider.TryGetComponent(out DeckCard overlappedCard))
            {
                if (CanPlaceCard(overlappedCard))
                {
                    overlappedCard.SelectCard();
                    _lastDragHit = overlappedCard;
                }
            }
        }

        private void DeselectLastDraggedCard()
        {
            if (!ReferenceEquals(_lastDragHit, null))
            {
                _lastDragHit.DeselectCard();
            }
        }

        private void TryPlaceSelf()
        {
            var hit = _raycaster.GetBoxcastNearestHit(transform);

            if (hit && TryInteractWithOverlappedCard(hit))
            {
                SetNewInitialPosition(hit.collider.transform.position);
                EventTurnEnded?.Invoke();
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
                
                if (CanPlaceCard(overlappedCard))
                {
                    InteractWithOverlappedCard(overlappedCard);
                    return true;
                }
                
                return false;
            }

            return false;
        }

        private bool CanPlaceCard(Card cardOnPosition)
        {
            if (GameStateGetter.State == TurnState.PlayerPositioningTurn)
            {
                return cardOnPosition.PositionData.Position.y == GameRulesGetter.Rules.PositioningStatePlacementsY;
            }

            return _positionChecker.CanPositionCard(cardOnPosition, PositionData.Position);
        }

        private void InteractWithOverlappedCard(Card card)
        {
            DeckCard cardComponent = card.GetComponent<DeckCard>();

            if (!ReferenceEquals(cardComponent, null))
            {
                if (cardComponent is EnemyCard enemy && CanInteract(enemy.Condition))
                {
                    enemy.Interact(this);
                    PlayParticleAttack();
                }
                else if (cardComponent.Condition == CardCondition.Alive)
                {
                    cardComponent.Interact(this);
                }
            }
            
            SetPosition(card.PositionData.Position);
        }

        private bool CanInteract(CardCondition condition)
        {
            return condition == CardCondition.Alive;
        }
    }
}