using DeckMaster;

namespace Data
{
    public static class PlayerStatsStorage
    {
        public static int Health;

        public static void Initialize()
        {
            if (Health <= 0)
            {
                Health = GameRulesGetter.Rules.PlayerMaxHealth;
            }
        }
    }
}