using System.Collections.Generic;
using Cards;
using DeckMaster.StateMachine;
using DefaultNamespace.Player;
using UnityEngine;

namespace DeckMaster
{
    public class DeckMaster : MonoBehaviour
    {
        [SerializeField] private PlayerHeroCard _player;
        [SerializeField] private PlayerInput _input;
        [SerializeField] private DeckSpawner _spawner;
        [SerializeField] private Vector2Int _visibleZone = new Vector2Int(2,2);

        private List<DeckCard> _deckCards;
        private List<Card> _placements;

        private State _currentState;

        private void OnEnable()
        {
            _player.EventTurnEnded += ChangeGameState;
        }

        private void OnDisable()
        {
            _player.EventTurnEnded -= ChangeGameState;
        }

        private void Start()
        {
            _deckCards = _spawner.SpawnCards();
            _currentState = new PlayerPositioningState(_input, _deckCards, _player, this, _spawner);
            _currentState =_currentState.Process();
            GameStateGetter.UpdateState(_currentState);
        }
        
        private void ChangeGameState(Vector2Int position, Card arg2)
        {
            _currentState.GetActualState();
            _currentState = _currentState.Process();
            GameStateGetter.UpdateState(_currentState);
        }
    }
}