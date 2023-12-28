using System.Collections.Generic;
using System.Linq;
using Cards;
using CardUtilities;
using Data;
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
        [SerializeField] private CameraScrolling _cameraScrolling;
        
        [SerializeField] private Vector2Int _visibleZone = new Vector2Int(2,2);
        [SerializeField] private Vector2Int _enemyAttackZone = Vector2Int.one;
        [SerializeField] private int _positioningStatePlacementsY = -1;
        [SerializeField] private int _playerMovingLimit = 1;
        [SerializeField] private int _playerMaxHealth = 12;
        [SerializeField] private bool _overhealWithDamage = true;
        [SerializeField] private List<RoomData> _possibleRooms;
        [SerializeField] private int _maxRooms = 2;

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
            GameRules.Initialize(_visibleZone,_positioningStatePlacementsY, _enemyAttackZone, _playerMovingLimit, _overhealWithDamage, _playerMaxHealth);
        }

        private void Start()
        {
            var converter = new DeckBuilder(_possibleRooms);
            
            _deckCards = _spawner.SpawnCards(converter.GetConcatinatedRooms());
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
            _cameraScrolling.SetTarget(_player.transform);
        }
    }
}