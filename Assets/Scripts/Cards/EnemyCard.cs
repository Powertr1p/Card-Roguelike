using System.Collections.Generic;
using CardUtilities;
using DefaultNamespace.Effects;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(DirectionAttacker))]
    public class EnemyCard : DeckCard
    {
        [SerializeField] private DirectionAttacker _directionAttacker;
        [SerializeField] private List<Effect> _effects;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void Interact(HeroCard heroCardConsumer)
        {
            PerformDeath();
        }

        public override void OpenCard()
        {
            _directionAttacker.SetAttackDirection();
            base.OpenCard();
        }
    }
}