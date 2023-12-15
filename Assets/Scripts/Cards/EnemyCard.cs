using System.Collections;
using System.Collections.Generic;
using CardUtilities;
using DefaultNamespace.Effects;
using DG.Tweening;
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

        public List<Vector2Int> GetTargetAttackPositions()
        {
            List<Vector2Int> positionToAttack = new List<Vector2Int>();
            
            foreach (var attackDirecton in _directionAttacker.AttackDirections)
            {
                var targetPosition = Data.Position + attackDirecton.GetAttackPosition;
                positionToAttack.Add(targetPosition);
            }

            return positionToAttack;
        }

        public IEnumerator PerformAttack(Vector3 position)
        {
            var animationEnded = false;
            var cachedTransform = transform;
            var initialPosition = cachedTransform.position;
            var offset = new Vector3(0, 0f, -0.15f);

            cachedTransform.DOMove(position + offset, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
            {
                transform.DOMove(initialPosition, 0.15f).SetEase(Ease.OutFlash);
                animationEnded = true;
            });

            yield return new WaitUntil(() => animationEnded);
        }

        public List<Effect> GetEffects()
        {
            return _effects;
        }
    }
}