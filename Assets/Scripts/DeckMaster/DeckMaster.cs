using System.Collections.Generic;
using Cards;
using CardUtilities;
using Data;
using DeckMaster.StateMachine;
using DefaultNamespace.Player;
using JetBrains.Annotations;
using Player;
using UnityEngine;

namespace DeckMaster
{
    public class DeckMaster : MonoBehaviour
    {
        [SerializeField, NotNull] private PlayerHeroCard _player;
        [SerializeField, NotNull] private PlayerInput _input;
        [SerializeField, NotNull] private DeckSpawner _spawner;
        [SerializeField, NotNull] private CameraScrolling _cameraScrolling;
        [SerializeField, NotNull] private GameRules _gameRules;

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

        private void Awake()
        {
            _gameRules.Initialize();
        }

        private void Start()
        {
            var converter = new DeckBuilder(GameRulesGetter.PossibleRooms, GameRulesGetter.MaxRooms);
            
            _deckCards = _spawner.SpawnCards(converter.GetConcatinatedRooms());
            SubscribeCardsDeath();
            _currentState = new PlayerPositioningState(_input, _deckCards, _player, this, _spawner);
            _currentState =_currentState.Process();
            
            _cameraScrolling.SetTarget(_player.transform, true);
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
            _cameraScrolling.SetTarget(_player.transform);
        }
    }
}