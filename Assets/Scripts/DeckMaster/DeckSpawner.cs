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
        
        [SerializeField] private Transform _firstRoom;

        [SerializeField] private Card _door;
        
        [SerializeField] private int _columns = 5;
        [SerializeField] private int _rows = 4;
        [SerializeField] private Vector2 _offset;

        public int Rows => _rows;

        public List<DeckCard> SpawnCards()
        {
            List<DeckCard> instancedCards = new List<DeckCard>();
        
            for (int i = 0; i < _columns; i++)
            {
                int nextPosition = GetStartPosition();
            
                for (int j = 0; j < _rows; j++)
                {
                    DeckCard card = CreateNewRandomCard(i, j, nextPosition, _firstRoom);
                    instancedCards.Add(card);
                
                    nextPosition += (int)_offset.x;
                }
            }
            
            return instancedCards;
        }

        public List<Card> SpawnPlacementsForPlayer()
        {
            List<Card> instancedPlacements = new List<Card>();
        
            for (int i = 0; i < 1; i++)
            {
                int nextPosition = GetStartPosition();

                for (int j = 0; j < _rows; j++)
                {
                    var placement = _placementFactory.CreateNewInstance(i - 1, j, nextPosition, _offset, _firstRoom);
                    instancedPlacements.Add(placement);

                    nextPosition += (int) _offset.x;
                }
            }

            return instancedPlacements;
        }

        public void SpawnCoins(Vector2Int deckPosition, Vector3 worldPosition)
        {
            _itemFactory.SpawnCoins(deckPosition, worldPosition);
        }

        private int GetStartPosition()
        {
            return -(_rows / 2) * (int)_offset.x;
        }
        
        private DeckCard CreateNewRandomCard(int col, int row, int position, Transform parent)
        {
            var rnd = Random.Range(0, 10);
            var instance = rnd > 5 ? CreateNewEnemyCard(col, row, position, parent) : CreateNewItemCard(col, row, position, parent);

            return instance;
        }

        private DeckCard CreateNewEnemyCard(int col, int row, int position, Transform parent)
        {
            return _enemyFactory.CreateNewInstance(col, row, position, _offset, parent);
        }

        private DeckCard CreateNewItemCard(int col, int row, int position, Transform parent)
        { 
            return _itemFactory.CreateNewInstance(col, row, position, _offset, parent);
        }
    }
}
