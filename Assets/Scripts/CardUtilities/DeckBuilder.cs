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
        private Vector2Int _doorPosition = Vector2Int.zero;
        
        public DeckBuilder(List<RoomData> roomDatas)
        {
            _roomDatas = roomDatas;
            _gridSize = GetGridSize();
        }

        public LevelCardType[,] GetConcatinatedRooms()
        {
            _levelGrid = new LevelCardType[_gridSize.y + 1, _gridSize.x + 1];

            foreach (var roomData in _roomDatas)
            { 
                CreateRoom(roomData);
            }

            return _levelGrid;
        }

        private void CreateRoom(RoomData roomData)
        {
            Debug.LogError($"Create Room: {roomData.name}");
            
            var roomCards = roomData.GetCards();
            
            for (int i = 0; i < roomCards.GetLength(0); i++)
            {
                for (int j = 0; j < roomCards.GetLength(1); j++)
                {
                    var currentColumn = _nextStartPos.y + i;
                    var currentRow = _nextStartPos.x + j;
                    
                    Debug.Log($"{currentColumn} {currentRow}");
                    
                    _levelGrid[currentColumn, currentRow] = roomCards[i, j];

                    if (roomCards[i, j] == LevelCardType.Door)
                    {
                        _doorPosition.x = currentRow;
                        _doorPosition.y = currentColumn;
                    }
                }
            }

            if (roomData.DoorAlignment == DoorAlignment.Right)
            {
                _nextStartPos.x = _doorPosition.x + 1;
                _nextStartPos.y = _doorPosition.y;
            }
            else if (roomData.DoorAlignment == DoorAlignment.Up)
            {
                _nextStartPos.x = _doorPosition.x;
                _nextStartPos.y = _doorPosition.y + 1;
            }
            else if (roomData.DoorAlignment == DoorAlignment.Left)
            {
                //_nextStartPos.x = _doorPosition
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
            
            return new Vector2Int(summRows, summCols);
        }
    }
}