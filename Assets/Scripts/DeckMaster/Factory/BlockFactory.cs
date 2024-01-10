using Cards;
using UnityEngine;

namespace DeckMaster.Factory
{
    public class BlockFactory : GenericFactory<BlockCard>
    {
        public override BlockCard CreateNewInstance(int col, int row, Vector2 worldPosition, Transform parent)
        {
            var instance = base.CreateNewInstance(col, row, worldPosition, parent);
            return instance;
        }
    }
}