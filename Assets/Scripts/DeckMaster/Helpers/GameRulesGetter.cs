using System.Collections.Generic;
using Data;
using UnityEngine;

namespace DeckMaster
{
    public static class GameRulesGetter
    {
        public static Vector2Int PlayerVisibleZone => _gameRules.VisibleZone;
        public static Vector2Int EnemyAttackZone => _gameRules.EnemyAttackZone;
        public static int PlacementsY => _gameRules.PositioningStatePlacementsY;
        public static int PlayerMovingLimit => _gameRules.PlayerMovingLimit;
        public static int PlayerMaxHealth => _gameRules.PlayerMaxHealth;
        public static bool OverhealWithDamage => _gameRules.OverhealWithDamage;

        public static List<RoomData> PossibleRooms => _gameRules.PossibleRooms;
        public static int MaxRooms => _gameRules.MaxRooms;

        public static float ScrollSpeed => _gameRules.ScrollSpeed;
        public static float LerpSpeed => _gameRules.LerpSpeed;
        public static float PositionThreshold => _gameRules.PositionThreshold;
        public static float OffsetY => _gameRules.OffsetY;
        public static float OffsetYOnGameStart => _gameRules.OffsetYOnGameStart;

        private static GameRules _gameRules;
        
        private static bool _isInitialized;
        
        public static void Initialize(GameRules gameRules)
        {
            if (_isInitialized) return;
            _isInitialized = true;

            _gameRules = gameRules;
        }
    }
}