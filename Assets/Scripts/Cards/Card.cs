using Data;
using UnityEngine;

namespace Cards
{
    public class Card : MonoBehaviour
    {
        private CardData _data;
        
        public void Initialize(Vector2Int position)
        {
            _data = new CardData(position);
        }
    }
}