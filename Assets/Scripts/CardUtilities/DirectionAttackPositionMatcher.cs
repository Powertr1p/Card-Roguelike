using System;
using Cards;
using UnityEngine;

namespace CardUtilities
{
    [Serializable]
    public struct DirectionAttackPositionMatcher
    {
        [SerializeField] private AttackDirection _attackDirection;
        [SerializeField] private Vector2Int _attackPosition;

        private Vector2Int GetAttackPosition()
        {
            return _attackPosition;
        }
    }
}