using System.Collections.Generic;
using Cards;
using DefaultNamespace.Effects;
using NaughtyAttributes;
using UnityEngine;

namespace DeckMaster.Factory
{
    public class EffectCardsFactory<T> : GenericFactory<T> where T : DeckCard
    {
        [BoxGroup("Effects Params")]
        [SerializeField] protected int MaxEffects = 1;
        [BoxGroup("Effects Params")]
        [SerializeField] protected List<Effect> PossibleEffects;
        
        public override T CreateNewInstance(int col, int row, int position, Vector2 offset)
        {
            var instance = base.CreateNewInstance(col, row, position, offset);
            var effects = SetRandomizeEffects();
            
            instance.SetEffects(effects);
            instance.InitializeVisuals(effects[0].VisualData.Icon);

            return instance;
        }

        private List<Effect> SetRandomizeEffects()
        {
            var effectsCount = Random.Range(1, MaxEffects);
            List<Effect> pickedEffects = new List<Effect>();

            for (int i = 0; i < effectsCount; i++)
            {
                var randomEffect = Random.Range(0, PossibleEffects.Count);
                pickedEffects.Add(PossibleEffects[randomEffect]);
            }

            return pickedEffects;
        }
    }
}