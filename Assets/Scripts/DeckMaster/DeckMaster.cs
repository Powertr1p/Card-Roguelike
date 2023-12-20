using System.Collections.Generic;
using Cards;
using DeckMaster.StateMachine;
using DefaultNamespace.Player;
using Player;
using UnityEngine;

namespace DeckMaster
{
    public class DeckMaster : MonoBehaviour
    {
        [SerializeField] private PlayerHeroCard _player;
        [SerializeField] private PlayerInput _input;
        [SerializeField] private DeckSpawner _spawner;
        [SerializeField] private Vector2Int _visibleZone = new Vector2Int(2,2);
        [SerializeField] private CameraScrolling _cameraScrolling;

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
            SubscribeCardsDeath();
            _currentState = new PlayerPositioningState(_input, _deckCards, _player, this, _spawner);
            _currentState =_currentState.Process();
        }

        private void SubscribeCardsDeath()
        {
            for (int i = 0; i < _deckCards.Count; i++)
            {
                _deckCards[i].DeathPerformed += OnEnemyDeath;
            }
        }

        private void OnEnemyDeath(object obj, DeathArgs deathArgs)
        {
            if (deathArgs.CanSpawnCoins)
            {
                deathArgs.Sender.gameObject.SetActive(false);
                _spawner.SpawnCoins(deathArgs.DeckPosition.Position, deathArgs.WorldPosition);
            }

            deathArgs.Sender.DeathPerformed -= OnEnemyDeath;
        }

        private void ChangeGameState()
        {
            if (GameStateGetter.State == TurnState.PlayerPositioningTurn) return;
            
            _cameraScrolling.SetTarget(_player.transform);
        }
    }
}