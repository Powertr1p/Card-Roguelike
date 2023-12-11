using Cards;
using UnityEngine;

namespace CardUtilities
{
    [System.Serializable]
    public struct DirectionAttackSpriteMatcher
    {
        [SerializeField] private SpriteRenderer _arrowSprite;
        [SerializeField] private AttackDirection _attackDirection;

        public SpriteRenderer GetSprite()
        {
            return _arrowSprite;
        }

        public AttackDirection GetAttackDirection()
        {
            return _attackDirection;
        }
    }
}