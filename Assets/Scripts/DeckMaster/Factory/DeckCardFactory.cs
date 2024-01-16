using Cards;
using Data;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace DeckMaster.Factory
{
    public class DeckCardFactory : GenericFactory<DeckCard>
    {
        public override DeckCard CreateNewInstance(int col, int row, Vector2 worldPosition, Transform parent, CardData data)
        {
            var instance = base.CreateNewInstance(col, row, worldPosition, parent, data);
            return instance;
        }
    }
}