using System;
using DefaultNamespace.Effects;
using DefaultNamespace.Effects.Enums;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(Health))]
    public class HeroCard : Card
    {
        private Health _health;
        private EffectMapper _effectMapper;

        private void Awake()
        {
            _health = GetComponent<Health>();
            
            _effectMapper = new EffectMapper(this);
        }

        public override void Interact(HeroCard interactorCard)
        {
        }

        public void ApplyEffect(Effect effect)
        {
            _effectMapper.GetEffect(effect).Invoke(effect.Amount, effect.AffectType);
        }

        public void Heal(int amount, AffectType affectType)
        {
            _health.IncreaseHealth(amount, affectType);
        }

        public void GetHpDamage(int amount, AffectType affectType)
        {
            _health.DecreaseHealth(amount, affectType);
        }

        public void AddShield(int amount, AffectType affectType)
        {
            _health.IncreaseShield(amount, affectType);
        }

        public void GetShieldDamage(int amount, AffectType affectType)
        {
            _health.DecreaseShield(amount, affectType);
        }
    }
}