using Cards;
using Data;
using DefaultNamespace.Effects;
using UnityEngine;

namespace DeckMaster.Factory
{
    public class ItemFactory : EffectCardsFactory<EffectCard>
    {
        [SerializeField] private Effect _coinsEffect;

        public void SpawnCoins(Vector3 worldPosition, CardData data)
        {
            var instance = base.CreateNewInstance();
            instance.Initialize(new CardData(data.Room, LevelCardType.Item), data.CardPositionData.Position);
            instance.SetEffect(_coinsEffect);
            instance.InitializeVisuals(_coinsEffect.VisualData);
            instance.transform.position = worldPosition;
        }
    }
}