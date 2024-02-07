using System.Collections.Generic;
using Cards;
using Data;
using DeckMaster.Factory;
using UnityEngine;

namespace DeckMaster
{
    public class DeckSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private PlacementFactory _placementFactory;
        [SerializeField] private ItemFactory _itemFactory;
        [SerializeField] private BlockFactory _blockFactory;
        [SerializeField] private DeckCardFactory _emptyCardFactory;
        [SerializeField] private Transform _firstRoom;
        [SerializeField] private DoorCard _door;
        [SerializeField] private HeroCard _boss;
        
        [SerializeField] private int _rows = 4;
        
        [SerializeField] private Vector2 _offset;

        public Vector2 Offset => _offset;
        
        private CardData[,] _currentPreset;
        private List<DeckCard> _firstRow = new List<DeckCard>();

        public List<DeckCard> SpawnCards(CardData[,] cards)
        {
            _currentPreset = cards;

            DoorCard lastDoor = null;
            List<DeckCard> instancedCards = new List<DeckCard>();

            bool isFirst = true;
            
            for (int i = 0; i < cards.GetLength(0); i++)
            {
                Vector2 nextPosition = GetStartPosition();
                nextPosition = new Vector2(nextPosition.x, i * _offset.y);

                for (int j = 0; j < cards.GetLength(1); j++)
                {
                    if (cards[i,j].Type != LevelCardType.Unreachable)
                    {
                        if (cards[i, j].Type == LevelCardType.Door)
                        {
                            var currentDoor =  CreateDoor(j, i, nextPosition, cards[i,j]);
                            
                            if (lastDoor == null)
                                lastDoor = currentDoor;

                            if (currentDoor.Room > lastDoor.Room)
                                lastDoor = currentDoor;
                        }
                        else
                        {
                            DeckCard card = CreateNewRandomCard(i, j, nextPosition, _firstRoom, cards[i,j]);
                            instancedCards.Add(card);

                            if (cards[i, j].Room == 1 && cards[i, j].Type != LevelCardType.Unreachable && isFirst)
                            {
                                _firstRow.Add(card);
                            }
                        }
                    }
                    
                    nextPosition = new Vector2(nextPosition.x + _offset.x, i * _offset.y);
                }

                if (_firstRow.Count > 0)
                {
                    isFirst = false;
                }
            }
            
            CreateBoss(lastDoor);

            return instancedCards;
        }

        public List<Card> SpawnPlacementsForPlayer()
        {
            List<Card> instancedPlacements = new List<Card>();

            for (int i = 0; i < 1; i++)
            {
                Vector2 nextPosition = GetPlacementsStartPosition();
                nextPosition = new Vector2(nextPosition.x, (_firstRow[i].Data.Position.y - 1) * _offset.y);

                for (int j = 0; j < _firstRow.Count; j++)
                {
                    var data = new CardData(1, LevelCardType.Empty, new Vector2Int(_firstRow[j].Data.Position.x, _firstRow[i].Data.Position.y - 1));
                    
                    var placement = _placementFactory.CreateNewInstance(_firstRow[i].Data.Position.y - 1, _firstRow[j].Data.Position.x, nextPosition, _firstRoom, data);
                    instancedPlacements.Add(placement);

                    nextPosition = new Vector2(nextPosition.x + _offset.x, (_firstRow[i].Data.Position.y - 1) * _offset.y);
                }
            }

            return instancedPlacements;
        }

        private Vector2 GetPlacementsStartPosition()
        {
            return new Vector2(_firstRow[0].transform.position.x, _firstRow[0].transform.position.y);
        }
        
        private DoorCard CreateDoor(int j, int i, Vector2 nextPosition, CardData data)
        {
            var door = Instantiate(_door, _firstRoom);
            
            data.SetNewPosition(new Vector2Int(j,i));
            door.Initialize(data);
            door.transform.position = new Vector3(nextPosition.x, i * _offset.y);

            return door;
        }

        private Vector2 GetStartPosition()
        {
            return new(-(_rows / 2) * (int)_offset.x, _offset.y);
        }
        
        private DeckCard CreateNewRandomCard(int col, int row, Vector2 position, Transform parent, CardData data)
        {
            DeckCard instance = _currentPreset[col, row].Type switch
            {
                LevelCardType.Item => CreateNewItemCard(col, row, position, parent, data),
                LevelCardType.Enemy => CreateNewEnemyCard(col, row, position, parent, data),
                LevelCardType.Block => CreateNewBlock(col, row, position, parent, data),
                LevelCardType.Empty => CreateNewEmpty(col, row, position, parent, data),
                _ => null
            };

            return instance;
        }

        private DeckCard CreateNewEnemyCard(int col, int row, Vector2 position, Transform parent, CardData data)
        {
            var enemy =  _enemyFactory.CreateNewInstance(col, row, position, parent, data);
            enemy.SetCoin(_itemFactory.SpawnCoins(enemy.transform.position, enemy.Data));

            return enemy;
        }

        private DeckCard CreateNewItemCard(int col, int row, Vector2 position, Transform parent,  CardData data)
        { 
            return _itemFactory.CreateNewInstance(col, row, position, parent, data);
        }

        private DeckCard CreateNewBlock(int col, int row, Vector2 position, Transform parent,  CardData data)
        {
            return _blockFactory.CreateNewInstance(col, row, position, parent, data);
        }

        private DeckCard CreateNewEmpty(int col, int row, Vector2 position, Transform parent,  CardData data)
        {
            return _emptyCardFactory.CreateNewInstance(col, row, position, parent, data);
        }

        private void CreateBoss(DoorCard door)
        {
           var boss = Instantiate(_boss);
           boss.Initialize(door.Data);
           boss.transform.position = door.transform.position;
           Destroy(door.gameObject);
        }
    }
}
