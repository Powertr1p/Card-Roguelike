using UnityEngine;

namespace Cards
{
    public abstract class ItemCard : Card
    {
        public override void Interact(HeroCard interactor)
        {
            ApplyItemEffectOnInteractor(interactor);
        }

        protected abstract void ApplyItemEffectOnInteractor(HeroCard interactor);
    }
}