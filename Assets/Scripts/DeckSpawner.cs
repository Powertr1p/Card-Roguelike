using System.Collections.Generic;
using Cards;
using UnityEngine;

public class DeckSpawner : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private int _columns = 5;
    [SerializeField] private int _rows = 4;
    [SerializeField] private Vector2 _offset;

    public List<Card> SpawnCards()
    {
        List<Card> instancedCards = new List<Card>();
        
        for (int i = 0; i < _columns; i++)
        {
            int nextPosition = GetStartPosition();
            
            for (int j = 0; j < _rows; j++)
            {
                var card =  CreateCard(i,j, nextPosition);
                instancedCards.Add(card);
                
                nextPosition += (int)_offset.x;
            }
        }

        return instancedCards;
    }

    private int GetStartPosition()
    {
        return -(_rows / 2) * (int)_offset.x;
    }

    private Card CreateCard(int col, int row, int position)
    {
        var instance = Instantiate(_cardPrefab);
        instance.Initialize(new Vector2Int(row, col));
        instance.transform.position = new Vector3(position, col * _offset.y);

        return instance;
    }
}
