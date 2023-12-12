using System;
using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace CardUtilities
{
    public class DirectionAttackSpriteRenderer : MonoBehaviour
    {
        [SerializeField] private List<DirectionAttackSprite> _attackArrows;

        private void Awake()
        {
            for (int i = 0; i < _attackArrows.Count; i++)
            {
                _attackArrows[i].GetSprite().gameObject.SetActive(false);
            }
        }

        public void EnableArrow(AttackDirection direction)
        {
            for (int i = 0; i < _attackArrows.Count; i++)
            {
                if (_attackArrows[i].GetAttackDirection() == direction)
                {
                    _attackArrows[i].GetSprite().gameObject.SetActive(true);
                }
            }
        }
    }
}