namespace Cards
{
    public sealed class DoorCard : DeckCard
    {
        protected override void Awake()
        {
            base.Awake();
            //Facing = FaceSate.FaceUp;
        }

        public override void Interact(HeroCard heroCardConsumer)
        {
        }
    }
}