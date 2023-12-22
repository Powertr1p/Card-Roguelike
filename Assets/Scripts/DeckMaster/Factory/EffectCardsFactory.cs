using System.Collections.Generic;
using Cards;
using DefaultNamespace.Effects;
using UnityEngine;

namespace DeckMaster.Factory
{
    public class EffectCardsFactory<T> : GenericFactory<T> where T : DeckCard
    {
        [SerializeField] protected List<Effect> PossibleEffects;
        
        public override T CreateNewInstance(int col, int row, int position, Vector2 offset, Transform parent)
        {
            var instance = base.CreateNewInstance(col, row, position, offset, parent);
            var effect = SetRandomizeEffect();
            
            instance.SetEffect(effect);
            instance.InitializeVisuals(effect.VisualData);

            return instance;
        }

        private Effect SetRandomizeEffect()
        {
            Effect pickedEffect;
            
            var randomEffect = Random.Range(0, PossibleEffects.Count);
            pickedEffect = PossibleEffects[randomEffect];
            
            return pickedEffect;
        }
    }
}