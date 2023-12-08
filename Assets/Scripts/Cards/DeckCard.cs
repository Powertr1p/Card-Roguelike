using DG.Tweening;
using UnityEngine;

namespace Cards
{
    public abstract class DeckCard : Card
    {
        [SerializeField] private GameObject _mainSpritesContainer;
        [SerializeField] private GameObject _deathSpritesContainer;

        private Transform _transform;

        public FaceSate Facing => _facing;
        public CardCondition Condition => _condition;
        
        private FaceSate _facing = FaceSate.FaceDown;
        private CardCondition _condition = CardCondition.Alive;

        protected virtual void Awake()
        {
            _transform = transform;
        }

        public virtual void OpenCard()
        {
            _facing = FaceSate.FaceUp;

            _transform.DORotate(Vector3.zero, 0.25f);
        }

        protected void PerformDeath()
        {
            _mainSpritesContainer.SetActive(false);
            _deathSpritesContainer.SetActive(true);
            
            _condition = CardCondition.Dead;
        }
    }
}