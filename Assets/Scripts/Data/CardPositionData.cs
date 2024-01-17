using UnityEngine;

namespace Data
{
    [System.Serializable]
    public struct CardPositionData
    {
        public Vector2Int Position;

        public CardPositionData(Vector2Int position)
        {
            Position = position;
        }
    }
}