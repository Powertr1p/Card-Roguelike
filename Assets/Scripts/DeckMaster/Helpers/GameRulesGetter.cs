using Data;

namespace DeckMaster
{
    public static class GameRulesGetter
    {
        public static GameRules Rules => _gameRules;
        
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