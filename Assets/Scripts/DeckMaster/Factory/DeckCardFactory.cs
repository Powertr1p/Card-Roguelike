using Cards;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace DeckMaster.Factory
{
    public class DeckCardFactory : GenericFactory<DeckCard>
    {
        public override DeckCard CreateNewInstance(int col, int row, Vector2 worldPosition, Transform parent)
        {
            var instance = base.CreateNewInstance(col, row, worldPosition, parent);
            return instance;
        }
    }
}