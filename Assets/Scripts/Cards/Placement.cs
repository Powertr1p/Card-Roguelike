namespace Cards
{
    public class Placement : DeckCard
    {
        protected override void Start()
        {
            MainSpritesContainer.SetActive(true);
        }

        public override void Interact(HeroCard heroCardConsumer)
        {
        }

        protected override void PerformDeath()
        {
            MainSpritesContainer.SetActive(false);
        }
    }
}