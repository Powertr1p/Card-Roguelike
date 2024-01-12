using System.Collections.Generic;
using Data;
using UnityEngine;

namespace DeckMaster
{
    public static class GameRulesGetter
    {
        public static Vector2Int PlayerVisibleZone { get; private set; }
        public static Vector2Int EnemyAttackZone { get; private set; }
        public static int PlacementsY { get; private set; }
        public static int PlayerMovingLimit { get; private set; }
        public static int PlayerMaxHealth { get; private set; }
        public static bool OverhealWithDamage { get; private set; }
        
        public static List<RoomData> PossibleRooms { get; private set; }
        public static int MaxRooms { get; private set; }

        private static bool _isInitialized;

        public static void Initialize(GameRules gameRules)
        {
            if (_isInitialized) return;
            _isInitialized = true;
            
            PlayerVisibleZone = gameRules.VisibleZone;
            PlacementsY = gameRules.PositioningStatePlacementsY;
            EnemyAttackZone = gameRules.EnemyAttackZone;
            PlayerMovingLimit = gameRules.PlayerMovingLimit;
            OverhealWithDamage = gameRules.OverhealWithDamage;
            PlayerMaxHealth = gameRules.PlayerMaxHealth;
            MaxRooms = gameRules.MaxRooms;
            PossibleRooms = gameRules.PossibleRooms;
        }
    }
}