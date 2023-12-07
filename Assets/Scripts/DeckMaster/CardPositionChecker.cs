using Cards;
using UnityEngine;

namespace DeckMaster
{
    public class CardPositionChecker
    {
        public bool CanPositionCard(Vector2Int desirePosition, Vector2Int currentPosition)
        {
            return (desirePosition.x >= currentPosition.x - 1 && desirePosition.x <= currentPosition.x + 1) &&
                   (desirePosition.y >= currentPosition.y - 1 && desirePosition.y <= currentPosition.y + 1);
        }
    }
}