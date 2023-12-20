using System;
using System.Collections.Generic;
using Data;
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
        [BoxGroup("Visual Dependencies")] 
        [SerializeField] private SpriteRenderer _frameRenderer;
        
        [FormerlySerializedAs("_mainSpritesContainer")] [SerializeField] protected GameObject MainSpritesContainer;
        [FormerlySerializedAs("_deathSpritesContainer")] [SerializeField] protected GameObject DeathSpritesContainer;
        
        public event EventHandler<DeathArgs> DeathPerformed;
        
        public bool CanSpawnCoinsOnDeath { get; protected set; } = false;
        
        public int EffectPower => Effect.Amount;
        
        protected Effect Effect;

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

        protected virtual void PerformDeath()
        {
            DeathSpritesContainer.SetActive(true);
            MainSpritesContainer.SetActive(false);

            DeathHandler(new DeathArgs(CanSpawnCoinsOnDeath, _transform.position, PositionData, this));
            
            _condition = CardCondition.Dead;
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