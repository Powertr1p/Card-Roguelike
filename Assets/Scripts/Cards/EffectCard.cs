namespace Cards
{
    public class EffectCard : DeckCard
    {
        public override void Interact(HeroCard interactor)
        {
            ApplyItemEffectOnInteractor(interactor);
        }

        protected void ApplyItemEffectOnInteractor(HeroCard interactor)
        {
            interactor.ApplyEffect(Effect);
            PerformDeath();
        }
    }
}