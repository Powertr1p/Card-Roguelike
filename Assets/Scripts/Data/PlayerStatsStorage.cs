using DeckMaster;

namespace Data
{
    public static class PlayerStatsStorage
    {
        public static int Health;
        public static int Shield;

        public static void Initialize()
        {
            if (Health <= 0)
            {
                Health = GameRulesGetter.Rules.PlayerMaxHealth;
                Shield = 0;
            }
        }
    }
}