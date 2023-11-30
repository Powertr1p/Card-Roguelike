using UnityEngine;

namespace Cards
{
    public class HealingPotionCard : ItemCard
    {
        [SerializeField] private int _amount = 5; 
        
        protected override void ApplyItemEffectOnInteractor(HeroCard interactor)
        {
            interactor.Heal(_amount);
        }
    }
}