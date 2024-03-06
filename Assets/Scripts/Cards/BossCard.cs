namespace Cards
{
    public class BossCard : DeckCard
    {
        public override void Interact(HeroCard heroCardConsumer)
        {
            PerformDeath();
            SendDeathEvent();
        }
    }
}