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
        [BoxGroup("Player Params"), PropertyTooltip("$VisibleZoneTooltip")]
        [SerializeField] private Vector2Int _visibleZone = new Vector2Int(2,2);
        [BoxGroup("Player Params"), PropertyTooltip("$PlayerMovingLimitTooltip")]
        [SerializeField] private int _playerMovingLimit = 1;
        [BoxGroup("Player Params"), PropertyTooltip("$PlayerMaxHealthTooltip")]
        [SerializeField] private int _playerMaxHealth = 12;
        [BoxGroup("Player Params"), PropertyTooltip("$OverhealWithDamageTooltip")]
        [SerializeField] private bool _overhealWithDamage = true;
        [BoxGroup("Player Params")]
        [SerializeField] private bool _isBackTracking = true;

        [BoxGroup("Player Feel"), PropertyTooltip("$BackOnCellAnimDurationTooltip")] 
        [SerializeField] private float _backOnCellAnimDuration = 0.25f;
        [BoxGroup("Player Feel"), PropertyTooltip("$CardScaleOnPlayerHoverTooltip")] 
        [SerializeField] private Vector3 _cardScaleOnPlayerHover = new Vector3(0.9f, 0.9f, 0.9f);
        
        [BoxGroup("Enemy"), PropertyTooltip("$EnemyAttackZoneTooltip")]
        [SerializeField] private Vector2Int _enemyAttackZone = Vector2Int.one;
        
        [BoxGroup("Deck"), PropertyTooltip("$PossibleRoomsTooltip")] 
        [SerializeField, NotNull] private List<RoomData> _possibleRooms;
        [BoxGroup("Deck"), PropertyTooltip("$MaxRoomsTooltip")] 
        [SerializeField] private int _maxRooms = 3;

        [BoxGroup("Deck Feel"), PropertyTooltip("$DelayBetweenCardsOpenTooltip")] 
        [SerializeField] private float _delayBetweenCardsOpen = 0.1f;
        [BoxGroup("Deck Feel"), PropertyTooltip("$CardOpenSpeedTooltip")] 
        [SerializeField] private float _cardOpenSpeed = 0.25f;

        [BoxGroup("Camera")] 
        [SerializeField] private bool _cameraFollow;
        [BoxGroup("Camera"), PropertyTooltip("$CameraFollowSpeedTooltip"), ShowIf("_cameraFollow")]
        [SerializeField] private float _cameraFollowSpeed = 3.0f;
        [BoxGroup("Camera"), PropertyTooltip("$PositionThresholdTooltip"), ShowIf("_cameraFollow")]
        [SerializeField] private float _positionThreshold = 0.1f;
        [BoxGroup("Camera"), PropertyTooltip("$OffsetYFromPlayerCardTooltip"), ShowIf("_cameraFollow")]
        [SerializeField] private float _offsetYFromPlayerCard = 0f;
        [BoxGroup("Camera"), PropertyTooltip("$MouseWheelZoomSpeedTooltip"), Space(10f)]
        [SerializeField] private float _mouseWheelZoomSpeed = 2.0f;
        [BoxGroup("Camera")] 
        [SerializeField] private float _maxZoomValue = -9f;
        [BoxGroup("Camera")] 
        [SerializeField] private float _minZoomValue = -21f;
        [BoxGroup("Camera"), PropertyTooltip("$OffsetYOnGameStartTooltip")]
        [SerializeField] private float _offsetYOnGameStart = 3f;

        [BoxGroup("Random")] 
        [SerializeField] private float _enemySpawnChance = 0.1f;
        [BoxGroup("Random")] 
        [SerializeField] private float _itemSpawnChance = 0.1f;
        [BoxGroup("Random")] 
        [SerializeField] private float _emptySpawnChance = 0.1f;
        [BoxGroup("Random")] 
        [SerializeField] private float _blockSpawnChance = 0.1f;

        public Vector3 VFXOffset { get; } = new(0, 0, -0.15f);
        
        public Vector2Int VisibleZone => _visibleZone;
        public int PlayerMovingLimit => _playerMovingLimit;
        public int PlayerMaxHealth => _playerMaxHealth;
        public bool OverhealWithDamage => _overhealWithDamage;
        public bool IsBackTracking => _isBackTracking;

        public float PlaceInititialPositionDuration => _backOnCellAnimDuration;
        public Vector3 HoverScaleValue => _cardScaleOnPlayerHover;
        public float DelayBetweenCardsOpen => _delayBetweenCardsOpen;
        public float CardsOpenSpeed => _cardOpenSpeed;

        public Vector2Int EnemyAttackZone => _enemyAttackZone;
        
        public List<RoomData> PossibleRooms => _possibleRooms;
        public int MaxRooms => _maxRooms;

        public bool CameraFollow => _cameraFollow;
        public float ScrollSpeed => _mouseWheelZoomSpeed;
        public float MaxZoomValue => _maxZoomValue;
        public float MinZoomValue => _minZoomValue;

        public float LerpSpeed => _cameraFollowSpeed;
        public float PositionThreshold => _positionThreshold;
        public float OffsetY => _offsetYFromPlayerCard;
        public float OffsetYOnGameStart => _offsetYOnGameStart;

        public float EnemySpawnChance => _enemySpawnChance;
        public float ItemSpawnChance => _itemSpawnChance;
        public float EmptySpawnChance => _emptySpawnChance;
        public float BlockSpawnChance => _blockSpawnChance;
        
         #region Tooltips
#if UNITY_EDITOR
        private const string VisibleZoneTooltip = "Радиус открытия карт вокруг позиции игрока";
        private const string PlayerMovingLimitTooltip = "Радиус ходьбы игрока";
        private const string PlayerMaxHealthTooltip = "Максимальное значение здоровья, которое дается при старте игры игроку. Игрок не может получить больше здоровья, чем данное значение. ИСКЛЮЧЕНИЕ: если включен Overheal";
        private const string OverhealWithDamageTooltip = "Если включено, то игрок может получить здоровье выше максимального, но за каждый ход будет сниматься по 1 едеинице здоровья";
        private const string BackOnCellAnimDurationTooltip = "Скорость возвращения карты на текущее место при неправильном ходе";
        private const string CardScaleOnPlayerHoverTooltip = "Какой размер принимает карта на доске, когда игрок драгает на нее свою карту. Работает только с теми картами, на которые игрок может сходить";
        private const string EnemyAttackZoneTooltip = "Радиус атаки врага со стрелочками";
        private const string PossibleRoomsTooltip = "Список комнат, которые могут быть построены";
        private const string MaxRoomsTooltip = "Максимальное кол-во комнат, которое может бысть построенно. Не должно превышать значение параметра PossibleRooms!";
        private const string DelayBetweenCardsOpenTooltip = "Задержка между открытием каждой следующей карты после хода игрока";
        private const string CardOpenSpeedTooltip = "Скорость переворачивания каждой карты после ходы игрока";
        private const string MouseWheelZoomSpeedTooltip = "Скорость зума колесика";
        private const string CameraFollowSpeedTooltip = "Скорость движения камеры после хода игрока. Чем выше, тем быстрее";
        private const string PositionThresholdTooltip = "Минимальное расстояние для остановки камеры во время ее движения, после хода игрока. Не должно быть 0!";
        private const string OffsetYFromPlayerCardTooltip = "Свдиг по Y который применяется во время движения и остановки камеры. По сути определяет, как камеры будет расположена относительно игрока. Формула = playerPosition + offset";
        private const string OffsetYOnGameStartTooltip = "Сдвиг по Y относительно игрока на старте игры. Чтобы выставить нужное положение камеры до начала игры.";
        //private const string RandomCard = "Вероятности спавна той или иной карты пр поставлении префаба комнаты, если выбрана карта Random";
        // private const string Tooltip = "Радиус открытия карт вокруг позиции игрока";
        // private const string Tooltip = "Радиус открытия карт вокруг позиции игрока";
#endif    
    #endregion
        
        public void Initialize()
        {
            GameRulesGetter.Initialize( this);
        }
    }
}