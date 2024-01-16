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
        
        [SerializeField] private int _rows = 4;
        
        [SerializeField] private Vector2 _offset;

        public Vector2 Offset => _offset;
        
        private CardData[,] _currentPreset;
        private List<DeckCard> _firstRow = new List<DeckCard>();

        public int Rows;

        public List<DeckCard> SpawnCards(CardData[,] cards)
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
                    if (cards[i,j].Type != LevelCardType.Unreachable)
                    {
                        if (cards[i, j].Type == LevelCardType.Door)
                        {
                            CreateDoor(j, i, nextPosition, cards[i,j]);
                        }
                        else
                        {
                            DeckCard card = CreateNewRandomCard(i, j, nextPosition, _firstRoom, cards[i,j]);

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
                    var placement = _placementFactory.CreateNewInstance(i - 1, _firstRow[j].PositionData.Position.x, nextPosition, _firstRoom, new CardData(0, LevelCardType.Empty));
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
            return new Vector2(_firstRow[0].transform.position.x, _firstRow[0].transform.position.y);
        }
        
        private void CreateDoor(int j, int i, Vector2 nextPosition, CardData data)
        {
            var door = Instantiate(_door, _firstRoom);
            door.Initialize(data,new Vector2Int(j, i));
            door.transform.position = new Vector3(nextPosition.x, i * _offset.y);
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
            return _enemyFactory.CreateNewInstance(col, row, position, parent, data);
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
    }
}
