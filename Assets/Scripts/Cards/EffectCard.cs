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
                CreateEffectParticles(Effect.EffectParticle).Play();

            PerformDeath();
            SendDeathEvent();
        }

        private ParticleSystem CreateEffectParticles(ParticleSystem effectParticle)
        {
            return Instantiate(effectParticle, transform.position, Quaternion.identity);
        }
    }
}