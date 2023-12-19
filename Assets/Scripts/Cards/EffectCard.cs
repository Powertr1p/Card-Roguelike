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
            foreach (var effect in Effects)
            {
                interactor.ApplyEffect(effect);
            }

            PerformDeath();
        }
    }
}