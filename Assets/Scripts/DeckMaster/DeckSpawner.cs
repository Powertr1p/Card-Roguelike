using System.Collections.Generic;
using Cards;
using DeckMaster.Factory;
using NaughtyAttributes;
using UnityEngine;

namespace DeckMaster
{
    public class DeckSpawner : MonoBehaviour
    {
        [BoxGroup("Spawners")]
        [SerializeField] private EnemyFactory _enemyFactory;
        [BoxGroup("Spawners")]
        [SerializeField] private PlacementFactory _placementFactory;
        [BoxGroup("Spawners")]
        [SerializeField] private ItemFactory _itemFactory;

        [BoxGroup("Room Containers")] 
        [SerializeField] private Transform _firstRoom;

        [SerializeField] private Card _door;
        
        [BoxGroup("Spawn Params")]
        [SerializeField] private int _columns = 5;
        [BoxGroup("Spawn Params")]
        [SerializeField] private int _rows = 4;
        [BoxGroup("Spawn Params")]
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
