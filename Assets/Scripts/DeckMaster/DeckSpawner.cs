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
        [SerializeField] private Transform _firstRoom;
        [SerializeField] private DoorCard _door;
        
        [SerializeField] private int _columns = 5;
        [SerializeField] private int _rows = 4;
        
        [SerializeField] private Vector2 _offset;
        [SerializeField] private RoomsPresents _presets;
        
        private LevelCardType[,] _currentPreset;

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
                    if (_currentPreset[i, j] == LevelCardType.Door)
                    {
                        CreateDoor(j, i, nextPosition);
                    }
                    else
                    {
                        DeckCard card = CreateNewRandomCard(i, j, nextPosition, _firstRoom);
                        instancedCards.Add(card);
                    }
                    
                    nextPosition = new Vector2(nextPosition.x + _offset.x, i * _offset.y);
                }
            }
            
            return instancedCards;
        }

        private void CreateDoor(int j, int i, Vector2 nextPosition)
        {
            var door = Instantiate(_door, _firstRoom);
            door.InitializePosition(new Vector2Int(j, i));
            door.transform.position = new Vector3(nextPosition.x, i * _offset.y);
        }

        public List<Card> SpawnPlacementsForPlayer()
        {
            List<Card> instancedPlacements = new List<Card>();
        
            for (int i = 0; i < 1; i++)
            {
                Vector2 nextPosition = GetStartPosition();
                nextPosition = new Vector2(nextPosition.x, (i - 1) * _offset.y);

                for (int j = 0; j < _rows; j++)
                {
                    var placement = _placementFactory.CreateNewInstance(i - 1, j, nextPosition, _offset, _firstRoom);
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

        private Vector2 GetStartPosition()
        {
            return new(-(_rows / 2) * (int)_offset.x, _offset.y);
        }
        
        private DeckCard CreateNewRandomCard(int col, int row, Vector2 position, Transform parent)
        {
            DeckCard instance = null;
            
            if (_currentPreset[col, row] == LevelCardType.Item)
            {
                instance = CreateNewItemCard(col, row, position, parent);
            }
            else if (_currentPreset[col, row] == LevelCardType.Enemy)
            {
                instance = CreateNewEnemyCard(col, row, position, parent);
            }
            
            return instance;
        }

        private DeckCard CreateNewEnemyCard(int col, int row, Vector2 position, Transform parent)
        {
            return _enemyFactory.CreateNewInstance(col, row, position, _offset, parent);
        }

        private DeckCard CreateNewItemCard(int col, int row, Vector2 position, Transform parent)
        { 
            return _itemFactory.CreateNewInstance(col, row, position, _offset, parent);
        }
    }
}
