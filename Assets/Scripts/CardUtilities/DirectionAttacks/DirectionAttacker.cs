using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace CardUtilities
{
    public class DirectionAttacker : MonoBehaviour
    {
        [SerializeField] private DirectionAttackPositionaData _attackData;
        [SerializeField] private DirectionAttackSpriteRenderer _arrowSprites;

        public List<DirectionAttackPosition> AttackDirections => _attackDirections;

        private List<DirectionAttackPosition> _attackDirections = new List<DirectionAttackPosition>();

        public void SetAttackDirection()
        {
            AttackDirection direction = (AttackDirection)Random.Range(0, _attackData.GetPossibleDirectionsCount());
            Vector2Int positionAttack = _attackData.GetAttackPosition(direction);
            
            _attackDirections.Add(new DirectionAttackPosition(direction, positionAttack));
            _arrowSprites.EnableArrow(direction);
        }
    }
}