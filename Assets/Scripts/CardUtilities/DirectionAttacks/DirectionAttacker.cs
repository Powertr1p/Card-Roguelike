using System.Collections.Generic;
using UnityEngine;

namespace CardUtilities
{
    public class DirectionAttacker : MonoBehaviour
    {
        [SerializeField] private DirectionAttackSpriteRenderer _arrowSprites;

        public List<DirectionAttackPosition> AttackDirections => _attackDirections;

        private List<DirectionAttackPosition> _attackDirections = new List<DirectionAttackPosition>();

        public void SetAttackDirection(List<DirectionAttackPosition> attackDirections)
        {
            _attackDirections = new List<DirectionAttackPosition>(attackDirections);
        }

        public void EnableSprites()
        {
            for (int i = 0; i < _attackDirections.Count; i++)
            {
                _arrowSprites.EnableArrow(_attackDirections[i].Direction);
            }
        }
    }
}