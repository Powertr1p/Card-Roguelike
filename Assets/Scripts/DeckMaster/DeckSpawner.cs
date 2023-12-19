using System.Collections.Generic;
using Cards;
using DeckMaster.Factory;
using UnityEngine;

namespace DeckMaster
{
    public class DeckSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private Card _placementPrefab;
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
                    var card = CreateCard(i, j, nextPosition);
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
                    var placement = CreatePlayerPlacement();
                    PlaceObjectOnDeck(placement,i - 1, j, nextPosition);
                    instancedPlacements.Add(placement);

                    nextPosition += (int) _offset.x;
                }
            }

            return instancedPlacements;
        }

        private int GetStartPosition()
        {
            return -(_rows / 2) * (int)_offset.x;
        }

        private DeckCard CreateCard(int col, int row, int position)
        {
            return _enemyFactory.CreateNewInstance(col, row, position, _offset);
        }

        private Card CreatePlayerPlacement()
        {
            var instance = Instantiate(_placementPrefab);
            return instance;
        }

        private void PlaceObjectOnDeck(Card instance, int col, int row, int position)
        {
            instance.Initialize(new Vector2Int(row, col));
            instance.transform.position = new Vector3(position, col * _offset.y);
        }
    }
}
