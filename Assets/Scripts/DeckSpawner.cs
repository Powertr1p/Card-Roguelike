using System;
using UnityEngine;

public class DeckSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
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
                var instance = Instantiate(_cardPrefab);
                instance.transform.position = new Vector3(nextPosition, i * _offset.y);
                nextPosition += (int)_offset.x;
            }
        }
    }

    private int GetStartPosition()
    {
        return -(_columns / 2) * (int)_offset.x;
    }
}
