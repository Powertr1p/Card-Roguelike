using UnityEngine;

namespace DeckMaster
{
    public static class GameStateGetter
    {
        public static TurnState State => _state.Name;
        private static State _state;

        public static void UpdateState(State createdState)
        {
            _state = createdState;
        }
    }
}