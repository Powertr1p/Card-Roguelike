using System;
using System.Collections.Generic;
using DefaultNamespace.Effects;
using DefaultNamespace.Effects.Enums;

namespace Cards
{
    public class EffectMapper
    {
        private Dictionary<(AffectParameter, EffectType), Action<int, AffectType>> _effectMethods;

        public EffectMapper(HeroCard player)
        {
            _effectMethods = new Dictionary<(AffectParameter, EffectType), Action<int, AffectType>>
            {
                {(AffectParameter.Health, EffectType.Positive), player.Heal },
                {(AffectParameter.Shield, EffectType.Positive), player.AddShield },
                {(AffectParameter.Coins, EffectType.Positive), player.AddCoins },
                {(AffectParameter.Health, EffectType.Negative), player.GetHpDamage },
                {(AffectParameter.Shield, EffectType.Negative), player.GetShieldDamage },
            };
        }

        public Action<int, AffectType> GetEffect(Effect effect)
        {
            return _effectMethods.TryGetValue((effect.AffectParameter, effect.EffectType), out Action<int, AffectType> method) ? method : null;
        }
    }
}