using System.Collections.Generic;
using DefaultNamespace.Effects;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cards
{
    public abstract class DeckCard : Card
    {
        [BoxGroup("Visual Dependencies")] 
        [SerializeField] private SpriteRenderer _iconRenderer;
        
        [FormerlySerializedAs("_mainSpritesContainer")] [SerializeField] protected GameObject MainSpritesContainer;
        [FormerlySerializedAs("_deathSpritesContainer")] [SerializeField] protected GameObject DeathSpritesContainer;
        [SerializeField] protected List<Effect> Effects;

        private Transform _transform;

        public FaceSate Facing => _facing;
        public CardCondition Condition => _condition;
        
        private FaceSate _facing = FaceSate.FaceDown;
        private CardCondition _condition = CardCondition.Alive;

        protected virtual void Awake()
        {
            _transform = transform;
        }

        protected virtual void Start()
        {
            MainSpritesContainer.SetActive(true);
            DeathSpritesContainer.SetActive(false);
        }

        public void InitializeVisuals(Sprite sprite)
        {
            _iconRenderer.sprite = sprite;
        }
        
        public void SetEffects(List<Effect> effects)
        {
            Effects = effects;
        }

        public virtual void OpenCard()
        {
            _facing = FaceSate.FaceUp;

            _transform.DORotate(Vector3.zero, 0.25f);
        }

        protected virtual void PerformDeath()
        {
            DeathSpritesContainer.SetActive(true);
            MainSpritesContainer.SetActive(false);
            
            _condition = CardCondition.Dead;
        }
    }
}