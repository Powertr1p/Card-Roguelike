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
                interactor.PlayParticleEffectCard(Effect.EffectParticle);
            
            PerformDeath();
            SendDeathEvent();
        }
    }
}