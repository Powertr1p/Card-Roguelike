using System.Collections.Generic;
using DefaultNamespace.Effects;
using UnityEngine;

namespace Cards
{
    public class EnemyCard : DeckCard
    {
        [SerializeField] private List<Effect> _effects;
        [SerializeField] private List<Transform> _directionArrows;

        protected override void Awake()
        {
            base.Awake();

            foreach (var arrow in _directionArrows)
            {
                arrow.gameObject.SetActive(false);
            }
        }

        public override void Interact(HeroCard heroCardConsumer)
        {
            PerformDeath();
        }

        public override void OpenCard()
        {
            var random = Random.Range(0, _directionArrows.Count);
            
            _directionArrows[random].gameObject.SetActive(true);
            
            base.OpenCard();
        }
    }
}