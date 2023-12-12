using System;
using Cards;
using UnityEngine;

namespace CardUtilities
{
    [Serializable]
    public struct DirectionAttackPosition
    {
        [SerializeField] private AttackDirection _attackDirection;
        [SerializeField] private Vector2Int _attackPosition;

        public AttackDirection Direction => _attackDirection;
        public Vector2Int GetAttackPosition => _attackPosition;

        public DirectionAttackPosition(AttackDirection direction, Vector2Int position)
        {
            _attackDirection = direction;
            _attackPosition = position;
        }
    }
}