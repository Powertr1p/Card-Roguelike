using Cards;
using Data;
using UnityEngine;

namespace DeckMaster.Factory
{
    public class PlacementFactory : GenericFactory<Placement>
    {
        public override Placement CreateNewInstance(int col, int row, Vector2 worldPosition, Transform parent, CardData data)
        {
            var instance = base.CreateNewInstance(col, row, worldPosition, parent, data);
            return instance;
        }
    }
}