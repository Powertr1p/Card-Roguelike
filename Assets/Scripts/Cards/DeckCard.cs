using System;
using Data;
using DeckMaster;
using DefaultNamespace.Effects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cards
{
    public abstract class DeckCard : Card
    {
        [SerializeField] private SpriteRenderer _iconRenderer;
        [SerializeField] private SpriteRenderer _frameRenderer;
        
        [FormerlySerializedAs("_mainSpritesContainer")] 
        [SerializeField] protected GameObject MainSpritesContainer;
        [FormerlySerializedAs("_deathSpritesContainer")] 
        [SerializeField] protected GameObject DeathSpritesContainer;
        
        public event EventHandler<DeathArgs> DeathPerformed;

        public Effect EffectData => Effect;

        protected Effect Effect;
        protected bool CanSpawnCoinsOnDeath { get; set; } = false;
        
        public FaceSate Facing => _facing;
        public CardCondition Condition => _condition;
        
        private Transform _transform;
        private FaceSate _facing = FaceSate.FaceDown;
        private CardCondition _condition = CardCondition.Alive;
        private bool _isSelected;

        protected virtual void Awake()
        {
            _transform = transform;
        }

        protected virtual void Start()
        {
            MainSpritesContainer.SetActive(true);
            DeathSpritesContainer.SetActive(false);
        }

        public void InitializeVisuals(CardVisualData data)
        {
            _iconRenderer.sprite = data.Icon;
            _frameRenderer.sprite = data.Frame;
            _frameRenderer.color = data.FrameColor;
        }
        
        public void SetEffect(Effect effect)
        {
            Effect = effect;
        }

        public virtual void OpenCard()
        {
            _facing = FaceSate.FaceUp;

            _transform.DORotate(Vector3.zero, GameRulesGetter.Rules.CardsOpenSpeed);
        }

        public void SelectCard()
        {
            if (Condition == CardCondition.Dead) return;
            if (_isSelected) return;
            _isSelected = true;

            MainSpritesContainer.transform.localScale = GameRulesGetter.Rules.HoverScaleValue;
        }

        public void DeselectCard()
        {
            if (Condition == CardCondition.Dead) return;
            if (!_isSelected) return;
            _isSelected = false;
            
            MainSpritesContainer.transform.localScale = Vector3.one;
        }

        protected virtual void PerformDeath()
        {
            DeathSpritesContainer.SetActive(true);
            MainSpritesContainer.SetActive(false);
            _condition = CardCondition.Dead;
            
            if (!GameRulesGetter.Rules.IsBackTracking)
            {
                Destroy(gameObject);
            }
        }

        protected void SendDeathEvent()
        {
            DeathHandler(new DeathArgs(_transform.position, CardData, this));
        }
        
        protected virtual void DeathHandler(DeathArgs args)
        {
            DeathPerformed?.Invoke(this, args);
        }
    }
    
    public class DeathArgs : EventArgs
    {
        public Vector3 WorldPosition;
        public CardData Data;
        public DeckCard Sender;

        public DeathArgs(Vector3 worldPosition, CardData data, DeckCard sender)
        {
            WorldPosition = worldPosition;
            Data = data;
            Sender = sender;
        }
    }
}