using DeckMaster;
using UnityEngine;

namespace Cards
{
    public class EffectCard : DeckCard
    {
        public override void Interact(HeroCard interactor)
        {
            ApplyItemEffectOnInteractor(interactor);
        }

        private void ApplyItemEffectOnInteractor(HeroCard interactor)
        {
            interactor.ApplyEffect(Effect);

            if (!ReferenceEquals(Effect.EffectParticle, null))
            {
                CreateEffectParticles(Effect.EffectParticle).Play();
            }
            else
            {
                Debug.LogError($"There is no particle prefab on effect: {Effect.name}", gameObject);
            }
            
            PerformDeath();
            SendDeathEvent();
        }

        private ParticleSystem CreateEffectParticles(ParticleSystem effectParticle)
        {
            return Instantiate(effectParticle, transform.position + GameRulesGetter.Rules.VFXOffset, Quaternion.identity);
        }
    }
}