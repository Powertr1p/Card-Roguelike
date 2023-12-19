using System.Collections.Generic;
using Cards;
using DefaultNamespace.Effects;
using UnityEngine;

namespace DeckMaster.Factory
{
    public class EnemyFactory : GenericFactory<EnemyCard>
    {
        [SerializeField] private List<Effect> _possibleEffects;
        [SerializeField] private int _maxEffects = 1;

        public override DeckCard CreateNewInstance(int col, int row, int position, Vector2 offset)
        {
            var instance = base.CreateNewInstance(col, row, position, offset) as EnemyCard;
            instance.SetEffects(SetRandomizeEffects());

            return instance;
        }

        private List<Effect> SetRandomizeEffects()
        {
            var effectsCount = Random.Range(1, _maxEffects);
            List<Effect> pickedEffects = new List<Effect>();

            for (int i = 0; i < effectsCount; i++)
            {
                var randomEffect = Random.Range(0, _possibleEffects.Count);
                pickedEffects.Add(_possibleEffects[randomEffect]);
            }

            return pickedEffects;
        }
    }
}