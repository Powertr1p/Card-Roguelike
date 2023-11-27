using UnityEngine;

namespace Data
{
    [System.Serializable]
    public struct CardData
    {
        public Vector2Int Position => _position;
        
        private Vector2Int _position;

        public CardData(Vector2Int position)
        {
            _position = position;
        }
    }
}