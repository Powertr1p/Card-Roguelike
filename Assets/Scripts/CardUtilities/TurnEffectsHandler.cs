using System;
using System.Collections.Generic;
using DefaultNamespace.Effects;
using UnityEngine;

namespace CardUtilities
{
    public class TurnEffectsHandler : MonoBehaviour
    {
        private Dictionary<Effect, int> _turnEffects = new Dictionary<Effect, int>();

        public event Action<Effect> ExecuteTurnEffect;

        public void AddTurnEffect(Effect effect)
        {
            if (IsSameEffectAlreadyExists(effect))
                AddDurationToSameEffect(effect);
            else
                _turnEffects.Add(effect, effect.Duration);
        }

        public void TryExecuteEffects()
        {
            if (_turnEffects.Count <= 0) return;
            
            foreach (var effect in new List<Effect>(_turnEffects.Keys))
            {
                int remainingTurns = _turnEffects[effect];
                
                ExecuteTurnEffect?.Invoke(effect);
                
                remainingTurns--;

                if (remainingTurns <= 0)
                {
                    _turnEffects.Remove(effect);
                }
                else
                {
                    _turnEffects[effect] = remainingTurns;
                }
            }
        }

        private bool IsSameEffectAlreadyExists(Effect effect)
        {
            return _turnEffects.ContainsKey(effect);
        }

        private void AddDurationToSameEffect(Effect effect)
        {
            var duration = _turnEffects[effect];

            duration += effect.Duration;

            _turnEffects[effect] = duration;
        }
    }
}