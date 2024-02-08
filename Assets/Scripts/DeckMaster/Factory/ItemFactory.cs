using System.Collections.Generic;
using Cards;
using Data;
using DefaultNamespace.Effects;
using UnityEngine;

namespace DeckMaster.Factory
{
    public class ItemFactory : EffectCardsFactory<EffectCard>
    {
        [SerializeField] private List<Effect> _possibleEnemyDrop;

        public EffectCard SpawnDrop(Vector3 worldPosition, CardData data)
        {
            var instance = base.CreateNewInstance();
            instance.Initialize(new CardData(data.Room, LevelCardType.Item, data.Position));

            var rndEffect = GetRandomDrop();

            instance.SetEffect(rndEffect);
            instance.InitializeVisuals(rndEffect.VisualData);
            instance.transform.position = worldPosition;

            return instance;
        }

        private Effect GetRandomDrop()
        {
            return _possibleEnemyDrop[Random.Range(0, _possibleEnemyDrop.Count)];
        }
    }
}