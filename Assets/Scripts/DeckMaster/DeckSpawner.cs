using System.Collections.Generic;
using Cards;
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
        
        [SerializeField] private int _rows = 4;
        
        [SerializeField] private Vector2 _offset;

        public Vector2 Offset => _offset;
        
        private LevelCardType[,] _currentPreset;
        private List<DeckCard> _firstRow = new List<DeckCard>();

        public int Rows;

        public List<DeckCard> SpawnCards(LevelCardType[,] cards)
        {
            var rows = cards.GetLength(1);

            _currentPreset = cards;
            
            Rows = rows;
            
            List<DeckCard> instancedCards = new List<DeckCard>();
        
            for (int i = 0; i < cards.GetLength(0); i++)
            {
                Vector2 nextPosition = GetStartPosition();
                nextPosition = new Vector2(nextPosition.x, i * _offset.y);

                for (int j = 0; j < cards.GetLength(1); j++)
                {
                    if (cards[i,j] != LevelCardType.Unreachable && cards[i,j] != LevelCardType.Random)
                    {
                        if (cards[i, j] == LevelCardType.Door)
                        {
                            CreateDoor(j, i, nextPosition);
                        }
                        else
                        {
                            DeckCard card = CreateNewRandomCard(i, j, nextPosition, _firstRoom);

                            instancedCards.Add(card);

                            if (i == 0)
                            {
                                _firstRow.Add(card);
                            }
                        }
                    }
                    
                    nextPosition = new Vector2(nextPosition.x + _offset.x, i * _offset.y);
                }
            }
            
            return instancedCards;
        }

        public List<Card> SpawnPlacementsForPlayer()
        {
            List<Card> instancedPlacements = new List<Card>();
        
            for (int i = 0; i < 1; i++)
            {
                Vector2 nextPosition = GetPlacementsStartPosition();
                nextPosition = new Vector2(nextPosition.x, (i - 1) * _offset.y);

                for (int j = 0; j < _firstRow.Count; j++)
                {
                    var placement = _placementFactory.CreateNewInstance(i - 1, _firstRow[j].PositionData.Position.x, nextPosition, _firstRoom);
                    instancedPlacements.Add(placement);

                    nextPosition = new Vector2(nextPosition.x + _offset.x, (i - 1) * _offset.y);
                }
            }

            return instancedPlacements;
        }

        public void SpawnCoins(Vector2Int deckPosition, Vector3 worldPosition)
        {
            _itemFactory.SpawnCoins(deckPosition, worldPosition);
        }
        
        private Vector2 GetPlacementsStartPosition()
        {
            Debug.Log(_firstRow.Count);
            
            return new Vector2(_firstRow[0].transform.position.x, _firstRow[0].transform.position.y);
        }
        
        private void CreateDoor(int j, int i, Vector2 nextPosition)
        {
            var door = Instantiate(_door, _firstRoom);
            door.InitializePosition(new Vector2Int(j, i));
            door.transform.position = new Vector3(nextPosition.x, i * _offset.y);
        }

        private Vector2 GetStartPosition()
        {
            return new(-(_rows / 2) * (int)_offset.x, _offset.y);
        }
        
        private DeckCard CreateNewRandomCard(int col, int row, Vector2 position, Transform parent)
        {
            DeckCard instance = _currentPreset[col, row] switch
            {
                LevelCardType.Item => CreateNewItemCard(col, row, position, parent),
                LevelCardType.Enemy => CreateNewEnemyCard(col, row, position, parent),
                LevelCardType.Block => CreateNewBlock(col, row, position, parent),
                LevelCardType.Empty => CreateNewEmpty(col, row, position, parent),
                _ => null
            };

            return instance;
        }

        private DeckCard CreateNewEnemyCard(int col, int row, Vector2 position, Transform parent)
        {
            return _enemyFactory.CreateNewInstance(col, row, position, parent);
        }

        private DeckCard CreateNewItemCard(int col, int row, Vector2 position, Transform parent)
        { 
            return _itemFactory.CreateNewInstance(col, row, position, parent);
        }

        private DeckCard CreateNewBlock(int col, int row, Vector2 position, Transform parent)
        {
            return _blockFactory.CreateNewInstance(col, row, position, parent);
        }

        private DeckCard CreateNewEmpty(int col, int row, Vector2 position, Transform parent)
        {
            return _emptyCardFactory.CreateNewInstance(col, row, position, parent);
        }
    }
}
