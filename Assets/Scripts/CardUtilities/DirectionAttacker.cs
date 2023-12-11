using System.Collections.Generic;
using UnityEngine;

namespace CardUtilities
{
    public class DirectionAttacker : MonoBehaviour
    {
        [SerializeField] private List<DirectionAttackSpriteMatcher> _attackDirectionsList;
        [SerializeField] private List<DirectionAttackPositionMatcher> _attackPositionList;
    }
}