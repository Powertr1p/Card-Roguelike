using System.Collections.Generic;
using Data;
using UnityEngine;

namespace CardUtilities
{
    public class DeckBuilder
    {
        private List<RoomData> _roomDatas;
        private LevelCardType[,] _levelGrid;

        private Vector2Int _gridSize;
        private Vector2Int _nextStartPos = Vector2Int.zero;
        
        public DeckBuilder(List<RoomData> roomDatas)
        {
            _roomDatas = roomDatas;
            _gridSize = GetGridSize();
        }

        public LevelCardType[,] GetConcatinatedRooms()
        {
            _levelGrid = new LevelCardType[_gridSize.y + 1, _gridSize.x + 1];

            int currentColumn = 0;
            int currentRow = 0;

            foreach (var roomData in _roomDatas)
            {
                var roomCards = roomData.GetCards();
                CreateRoom(ref roomCards);
            }

            return _levelGrid;
        }

        private void CreateRoom(ref LevelCardType[,] roomCards)
        {
            for (int i = 0; i < roomCards.GetLength(0); i++)
            {
                for (int j = 0; j < roomCards.GetLength(1); j++)
                {
                    var currentColumn = _nextStartPos.y + i;
                    var currentRow = _nextStartPos.x + j;
                    
                    _levelGrid[currentColumn, currentRow] = roomCards[i, j];
                }
            }
        }

        private Vector2Int GetGridSize()
        {
            var summCols = 0;
            var summRows = 0;
            
            for (int i = 0; i < _roomDatas.Count; i++)
            {
                summCols += _roomDatas[i].GridSize.y;
                summRows += _roomDatas[i].GridSize.x;
            }
            
            Debug.Log($"Max is {summCols} {summRows}");

            return new Vector2Int(summRows, summCols);
        }
    }
}