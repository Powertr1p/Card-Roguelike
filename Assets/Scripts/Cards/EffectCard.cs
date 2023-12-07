using System.Collections.Generic;
using DefaultNamespace.Effects;
using UnityEngine;

namespace Cards
{
    public class EffectCard : DeckCard
    {
        [SerializeField] private List<Effect> _effects;

        public override void Interact(HeroCard interactor)
        {
            ApplyItemEffectOnInteractor(interactor);
        }

        protected void ApplyItemEffectOnInteractor(HeroCard interactor)
        {
            foreach (var effect in _effects)
            {
                interactor.ApplyEffect(effect);
            }
        }
    }
}