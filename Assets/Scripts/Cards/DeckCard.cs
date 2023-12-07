namespace Cards
{
    public abstract class DeckCard : Card
    {
        public FaceSate Facing => _facing;
        private FaceSate _facing = FaceSate.FaceDown;
    }
}