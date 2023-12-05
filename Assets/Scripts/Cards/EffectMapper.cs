using System;
using System.Collections.Generic;
using DefaultNamespace.Effects;
using DefaultNamespace.Effects.Enums;

namespace Cards
{
    public class EffectMapper
    {
        private Dictionary<(AffectParameter, EffectType), Action<int>> _effectMethods;

        public EffectMapper(HeroCard player)
        {
            _effectMethods = new Dictionary<(AffectParameter, EffectType), Action<int>>
            {
                {(AffectParameter.Health, EffectType.Positive), player.IncreaseHp },
                {(AffectParameter.Shield, EffectType.Positive), player.IncreaseShield },
                {(AffectParameter.Health, EffectType.Negative), player.DecreaseHp },
                {(AffectParameter.Shield, EffectType.Negative), player.DecreaseShield },
            };
        }

        public Action<int> GetEffect(Effect effect)
        {
            return _effectMethods.TryGetValue((effect.AffectParameter, effect.EffectType), out Action<int> method) ? method : null;
        }
    }
}