using System.Collections.Generic;
using DeckMaster;
using JetBrains.Annotations;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "GameRules", order = 0)]
    public class GameRules : ScriptableObject
    {
        [Header("Player")]
        [SerializeField] private Vector2Int _visibleZone = new Vector2Int(2,2);
        [SerializeField] private int _playerMovingLimit = 1;
        [SerializeField] private int _playerMaxHealth = 12;
        [SerializeField] private bool _overhealWithDamage = true;

        [Header("Enemy")]
        [SerializeField] private Vector2Int _enemyAttackZone = Vector2Int.one;

        [Header("Deck")] 
        [SerializeField, NotNull] private List<RoomData> _possibleRooms;
        [SerializeField] private int _maxRooms = 3;

        public Vector2Int VisibleZone => _visibleZone;
        public int PlayerMovingLimit => _playerMovingLimit;
        public int PlayerMaxHealth => _playerMaxHealth;
        public bool OverhealWithDamage => _overhealWithDamage;
        public int PositioningStatePlacementsY { get; } = -1;
        public Vector2Int EnemyAttackZone => _enemyAttackZone;
        public List<RoomData> PossibleRooms => _possibleRooms;
        public int MaxRooms => _maxRooms;

        public void Initialize()
        {
            GameRulesGetter.Initialize( this);
        }
    }
}