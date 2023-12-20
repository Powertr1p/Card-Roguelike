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
        [SerializeField] private EffectCard _coinsPrefab;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        public void SetAttackDirections(List<DirectionAttackPosition> attacks)
        {
            _directionAttacker.SetAttackDirection(attacks);
        }

        public override void Interact(HeroCard heroCardConsumer)
        {
            _coinsPrefab.Interact(heroCardConsumer);
            PerformDeath();
        }

        public override void OpenCard()
        {
            _directionAttacker.EnableSprites();
            base.OpenCard();
        }

        public List<Vector2Int> GetTargetAttackPositions()
        {
            List<Vector2Int> positionToAttack = new List<Vector2Int>();
            
            foreach (var attackDirecton in _directionAttacker.AttackDirections)
            {
                var targetPosition = PositionData.Position + attackDirecton.GetAttackPosition;
                positionToAttack.Add(targetPosition);
            }

            return positionToAttack;
        }

        public IEnumerator PerformAttack(Vector3 position, HeroCard target)
        {
            var animationEnded = false;
            var cachedTransform = transform;
            var initialPosition = cachedTransform.position;
            var offset = new Vector3(0, 0f, -0.15f);

            //TODO: секвенция
            
            cachedTransform.DOMove(position + offset, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
            {
                DealDamage(target);
                
                transform.DOMove(initialPosition, 0.15f).SetEase(Ease.OutFlash).OnComplete(() =>
                {
                    animationEnded = true;
                });
            });

            yield return new WaitUntil(() => animationEnded);
            
            SpawnCoins();
            PerformDeath();
            
            gameObject.SetActive(false);
        }
        
        private void SpawnCoins()
        {
            var instance = Instantiate(_coinsPrefab, transform.position, Quaternion.identity);
            instance.InitializePosition(positionData.Position);
        }

        private void DealDamage(HeroCard target)
        {
            target.ApplyEffect(Effect);
        }
    }
}