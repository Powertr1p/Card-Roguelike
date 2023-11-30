using DefaultNamespace.Interfaces;
using UnityEngine;

namespace Cards
{
    public class HeroCard : Card
    {
        [SerializeField] protected int Hp = 10;
        
        public override void Interact(HeroCard interactorCard)
        {
        }

        public void Heal(int amount)
        {
            Hp += amount;
        }
    }
}