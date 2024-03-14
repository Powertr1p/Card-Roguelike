using System.Collections;
using System.Collections.Generic;
using CardUtilities;
using DeckMaster;
using DefaultNamespace.Effects;
using DG.Tweening;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(DirectionAttacker))]
    public sealed class EnemyCard : DeckCard
    {
        [SerializeField] private DirectionAttacker _directionAttacker;
        [SerializeField] private EffectCard _coinDrop;

        protected override void Awake()
        {
            base.Awake();
            CanSpawnCoinsOnDeath = true;
        }

        public void SetAttackDirections(List<DirectionAttackPosition> attacks)
        {
            _directionAttacker.SetAttackDirection(attacks);
        }

        public void SetCoin(EffectCard coins)
        {
            _coinDrop = coins;
            _coinDrop.gameObject.SetActive(false);
        }

        public override void Interact(HeroCard heroCardConsumer)
        {
            PerformDeath();
            SendDeathEvent();

            var effect = _coinDrop.EffectData;
            heroCardConsumer.AddCoins(effect.Amount, effect);
            Destroy(_coinDrop.gameObject);
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
                var targetPosition = Data.Position + attackDirecton.GetAttackPosition;
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
            
            PerformDeath();
            SendDeathEvent(_coinDrop);
            
            gameObject.SetActive(false);
            _coinDrop.gameObject.SetActive(true);
        }

        private void DealDamage(HeroCard target)
        {
            target.ApplyEffect(Effect);
            SpawnEffectParticle(target);
        }

        private void SpawnEffectParticle(Card target)
        {
            var particleInstance = Instantiate(Effect.EffectParticle, target.transform.position + GameRulesGetter.Rules.VFXOffset, Quaternion.identity);
            particleInstance.Play();
        }
    }
}