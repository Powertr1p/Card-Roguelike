using System;
using Data;
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
        
        [FormerlySerializedAs("_mainSpritesContainer")] [SerializeField] protected GameObject MainSpritesContainer;
        [FormerlySerializedAs("_deathSpritesContainer")] [SerializeField] protected GameObject DeathSpritesContainer;
        
        public event EventHandler<DeathArgs> DeathPerformed;

        protected bool CanSpawnCoinsOnDeath { get; set; } = false;
        
        public int EffectPower => Effect.Amount;
        
        protected Effect Effect;

        private Transform _transform;
        private bool _isSelected;

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

        public void InitializeVisuals(CardVisualData data)
        {
            _iconRenderer.sprite = data.Icon;
            _frameRenderer.sprite = data.Frame;
        }
        
        public void SetEffect(Effect effect)
        {
            Effect = effect;
        }

        public virtual void OpenCard()
        {
            _facing = FaceSate.FaceUp;

            _transform.DORotate(Vector3.zero, 0.25f);
        }

        public void SelectCard()
        {
            if (Condition == CardCondition.Dead) return;
            if (_isSelected) return;
            _isSelected = true;

            MainSpritesContainer.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
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
        }

        protected void SendDeathEvent()
        {
            DeathHandler(new DeathArgs(CanSpawnCoinsOnDeath, _transform.position, PositionData, this));
        }
        
        protected virtual void DeathHandler(DeathArgs args)
        {
            DeathPerformed?.Invoke(this, args);
        }
    }
    
    public class DeathArgs : EventArgs
    {
        public bool CanSpawnCoins;
        public Vector3 WorldPosition;
        public CardPositionData DeckPosition;
        public DeckCard Sender;

        public DeathArgs(bool canSpawnCoins, Vector3 worldPosition, CardPositionData deckPosition, DeckCard sender)
        {
            CanSpawnCoins = canSpawnCoins;
            WorldPosition = worldPosition;
            DeckPosition = deckPosition;
            Sender = sender;
        }
    }
}