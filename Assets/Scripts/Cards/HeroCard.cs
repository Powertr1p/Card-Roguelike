using System;
using DefaultNamespace.Effects;
using DefaultNamespace.Effects.Enums;
using NaughtyAttributes;
using UnityEngine;

namespace Cards
{
    public class HeroCard : Card
    {
        [SerializeField] protected int Hp = 10;
        [SerializeField] protected int Shield = 0;

        private EffectMapper _effectMapper;

        private void Awake()
        {
            _effectMapper = new EffectMapper(this);
        }

        public override void Interact(HeroCard interactorCard)
        {
        }

        public void ApplyEffect(Effect effect)
        {
            _effectMapper.GetEffect(effect).Invoke(effect.Amount);
        }

        public void IncreaseHp(int amount)
        {
            Hp += amount;
        }

        public void IncreaseHp()
        {
            Debug.Log("SSSS");
        }

        public void DecreaseHp(int amount)
        {
            Hp -= amount;
        }

        public void IncreaseShield(int amount)
        {
            Shield += amount;
        }

        public void DecreaseShield(int amount)
        {
            Shield -= amount;
        }
    }
}