using System.Collections.Generic;
using DeckMaster;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "GameRules", order = 0)]
    public class GameRules : ScriptableObject
    {
        [BoxGroup("Player Params")]
        [SerializeField] private Vector2Int _visibleZone = new Vector2Int(2,2);
        [BoxGroup("Player Params")]
        [SerializeField] private int _playerMovingLimit = 1;
        [BoxGroup("Player Params")]
        [SerializeField] private int _playerMaxHealth = 12;
        [BoxGroup("Player Params")]
        [SerializeField] private bool _overhealWithDamage = true;

        [BoxGroup("Player Feel")] 
        [SerializeField] private float _backOnCellAnimDuration = 0.25f;
        [SerializeField] private Vector3 _cardScaleOnPlayerHover = new Vector3(0.9f, 0.9f, 0.9f);
        
        [BoxGroup("Enemy")]
        [SerializeField] private Vector2Int _enemyAttackZone = Vector2Int.one;
        
        [BoxGroup("Deck")] 
        [SerializeField, NotNull] private List<RoomData> _possibleRooms;
        [BoxGroup("Deck")] 
        [SerializeField] private int _maxRooms = 3;

        [BoxGroup("Deck Feel")] 
        [SerializeField] private float _delayBetweenCardsOpen = 0.1f;
        [SerializeField] private float _cardOpenSpeed = 0.25f;
        
        [BoxGroup("Camera")]
        [SerializeField] private float _mouseWheelZoomSpeed = 2.0f;
        [BoxGroup("Camera")]
        [SerializeField] private float _cameraFollowSpeed = 3.0f;
        [BoxGroup("Camera")]
        [SerializeField] private float _positionThreshold = 0.1f;
        [BoxGroup("Camera")]
        [SerializeField] private float _offsetYFromPlayerCard = 0f;
        [BoxGroup("Camera")]
        [SerializeField] private float _offsetYOnGameStart = 3f;

        public Vector2Int VisibleZone => _visibleZone;
        public int PlayerMovingLimit => _playerMovingLimit;
        public int PlayerMaxHealth => _playerMaxHealth;
        public bool OverhealWithDamage => _overhealWithDamage;
        public int PositioningStatePlacementsY { get; } = -1;

        public float PlaceInititialPositionDuration => _backOnCellAnimDuration;
        public Vector3 HoverScaleValue => _cardScaleOnPlayerHover;
        public float DelayBetweenCardsOpen => _delayBetweenCardsOpen;
        public float CardsOpenSpeed => _cardOpenSpeed;

        public Vector2Int EnemyAttackZone => _enemyAttackZone;
        
        public List<RoomData> PossibleRooms => _possibleRooms;
        public int MaxRooms => _maxRooms;

        public float ScrollSpeed => _mouseWheelZoomSpeed;
        public float LerpSpeed => _cameraFollowSpeed;
        public float PositionThreshold => _positionThreshold;
        public float OffsetY => _offsetYFromPlayerCard;
        public float OffsetYOnGameStart => _offsetYOnGameStart;
        

        public void Initialize()
        {
            GameRulesGetter.Initialize( this);
        }
    }
}