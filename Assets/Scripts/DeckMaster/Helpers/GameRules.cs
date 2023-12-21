using UnityEngine;

namespace DeckMaster
{
    public static class GameRules
    {
        public static Vector2Int PlayerVisibleZone { get; private set; }
        public static Vector2Int EnemyAttackZone { get; private set; }
        public static int PlacementsY { get; private set; }
        public static int PlayerMovingLimit { get; private set; }
        public static int PlayerMaxHealth { get; private set; }
        public static bool OverhealWithDamage { get; private set; }

        private static bool _isInitialized;
        
        public static void Initialize(Vector2Int playerVisibleZone, int yPlacements, Vector2Int enemyAttackZone, 
            int playerMovingLimit, bool overhealDamage, int playerMaxHealth)
        {
            if (_isInitialized) return;
            _isInitialized = true;

            PlayerVisibleZone = playerVisibleZone;
            PlacementsY = yPlacements;
            EnemyAttackZone = enemyAttackZone;
            PlayerMovingLimit = playerMovingLimit;
            OverhealWithDamage = overhealDamage;
            PlayerMaxHealth = playerMaxHealth;
        }
    }
}