using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace CardUtilities
{
    [CreateAssetMenu(fileName = "DirectionAttacks", menuName = "DirectionAttacks", order = 0)]
    public class DirectionAttackPositionaData : ScriptableObject
    {
        [SerializeField] private List<DirectionAttackPosition> _attackPositionMatcher;

        public Vector2Int GetAttackPosition(AttackDirection direction)
        {
            for (int i = 0; i < _attackPositionMatcher.Count; i++)
            {
                if (_attackPositionMatcher[i].Direction == direction)
                    return _attackPositionMatcher[i].GetAttackPosition;
            }

            return Vector2Int.zero;
        }

        public int GetPossibleDirectionsCount()
        {
            return _attackPositionMatcher.Count;
        }
    }
}