using System;
using Cards;
using UnityEngine;

public class DeckSpawner : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private int _columns = 5;
    [SerializeField] private int _rows = 4;
    [SerializeField] private Vector2 _offset;

    private void Start()
    {
       SpawnCards();
    }

    private void SpawnCards()
    {
        for (int i = 0; i < _rows; i++)
        {
            int nextPosition = GetStartPosition();
            
            for (int j = 0; j < _columns; j++)
            {
                CreateCard(i,j, nextPosition);
                
                nextPosition += (int)_offset.x;
            }
        }
    }

    private int GetStartPosition()
    {
        return -(_columns / 2) * (int)_offset.x;
    }

    private void CreateCard(int row, int col, int position)
    {
        var instance = Instantiate(_cardPrefab);
        instance.Initialize(new Vector2Int(row, col));
        instance.transform.position = new Vector3(position, row * _offset.y);
    }
}
