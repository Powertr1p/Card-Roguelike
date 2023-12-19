using UnityEngine;

namespace Data
{
    [System.Serializable]
    public struct CardPositionData
    {
        public Vector2Int Position => _position;
        
        private Vector2Int _position;

        public CardPositionData(Vector2Int position)
        {
            _position = position;
        }
    }
}