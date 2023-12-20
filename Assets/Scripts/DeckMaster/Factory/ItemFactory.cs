using Cards;
using DefaultNamespace.Effects;
using UnityEngine;

namespace DeckMaster.Factory
{
    public class ItemFactory : EffectCardsFactory<EffectCard>
    {
        [SerializeField] private Effect _coinsEffect;

        public void SpawnCoins(Vector2Int deckPosition, Vector3 worldPosition)
        {
            var instance = base.CreateNewInstance();
            instance.InitializePosition(deckPosition);
            instance.SetEffect(_coinsEffect);
            instance.InitializeVisuals(_coinsEffect.VisualData);
            instance.transform.position = worldPosition;
        }
    }
}