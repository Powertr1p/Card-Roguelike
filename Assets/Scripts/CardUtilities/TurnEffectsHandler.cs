using System.Collections.Generic;
using DefaultNamespace.Effects;
using UnityEngine;

namespace CardUtilities
{
    public class TurnEffectsHandler : MonoBehaviour
    {
        private Dictionary<Effect, int> _turnEffects = new Dictionary<Effect, int>();

        public void AddTurnEffect(Effect effect)
        {
            _turnEffects.Add(effect, effect.Duration);
        }

        public void TryExecuteEffects()
        {
            if (_turnEffects.Count <= 0) return;
            
            foreach (var effect in _turnEffects)
            {
                
            }
        }
    }
}