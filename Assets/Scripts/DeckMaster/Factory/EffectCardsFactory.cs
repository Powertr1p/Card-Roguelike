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
        [Space(5)]
        [SerializeField] protected int MaxEffects = 1;
        [BoxGroup("Effects Params")]
        [SerializeField] protected List<Effect> PossibleEffects;
        
        protected List<Effect> SetRandomizeEffects()
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