using System;
using System.Collections.Generic;
using Cards;
using CardUtilities;
using Data;
using DeckMaster.StateMachine;
using DefaultNamespace.Player;
using JetBrains.Annotations;
using Player;
using UI;
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
        [SerializeField, NotNull] private DeathScreen _deathScreen;

        public event Action EnemyDeath;
        
        private List<DeckCard> _deckCards;
        private List<Card> _placements;

        private State _currentState;

        private void OnEnable()
        {
            _player.EventTurnEnded += ChangeGameState;
            _player.OnHealthZero += HandlePlayerDeath;
        }

        private void OnDisable()
        {
            _player.EventTurnEnded -= ChangeGameState;
            _player.OnHealthZero -= HandlePlayerDeath;
        }

        private void Awake()
        {
            _gameRules.Initialize();
        }

        private void Start()
        {
            var converter = new DeckBuilder(GameRulesGetter.Rules.PossibleRooms, GameRulesGetter.Rules.MaxRooms);
            
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
                _deckCards[i].DeathPerformed += OnCardConsumed;
            }
        }

        private void OnCardConsumed(object obj, DeathArgs deathArgs)
        {
            if (deathArgs.Data.Type == LevelCardType.Enemy)
            {
                EnemyDeath?.Invoke();
            }
            
            deathArgs.Sender.DeathPerformed -= OnCardConsumed;
        }

        private void ChangeGameState()
        {
            if (!GameRulesGetter.Rules.CameraFollow) return;
            
            _cameraScrolling.SetTarget(_player.transform);
        }
        
        private void HandlePlayerDeath()
        {
            _deathScreen.AnimationComplete += RestartLevel;
            _deathScreen.Show();
        }
        
        private void RestartLevel()
        {
            _deathScreen.AnimationComplete -= RestartLevel;
            
            DelayedExecution.Call(1f, async () =>
            {
                SceneLoader.RestartScene();
            });
        }
    }
}