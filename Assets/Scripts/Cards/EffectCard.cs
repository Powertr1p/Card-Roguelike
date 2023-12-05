using System;
using System.Collections.Generic;
using DefaultNamespace.Effects;
using DefaultNamespace.Effects.Enums;
using UnityEngine;

namespace Cards
{
    public class EffectCard : Card
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