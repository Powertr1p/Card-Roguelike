using System.Collections.Generic;
using System.Linq;
using Data;
using Unity.VisualScripting;
using UnityEngine;

namespace CardUtilities
{
    public class DeckBuilder
    {
        private List<RoomData> _pickedRoomDatas;
        private List<RoomData> _allRooms;
        private LevelCardType[,] _levelGrid;
        private DoorAlignment _lastPickedDoor = DoorAlignment.Undefined;
        private DoorAlignment _excludeNextDoorAlignment = DoorAlignment.Undefined;

        private Vector2Int _gridSize;
        private Vector2Int _nextStartPos = new Vector2Int(15, 15);
        private Vector2Int _doorPosition = Vector2Int.zero;

        private bool _isLastLeft = false;
        
        public DeckBuilder(List<RoomData> allRooms)
        {
            _allRooms = allRooms;
            _pickedRoomDatas = RandomizeRooms();
            _gridSize = GetGridSize();
        }

        public LevelCardType[,] GetConcatinatedRooms()
        {
            _levelGrid = new LevelCardType[30, 30];

            foreach (var roomData in _pickedRoomDatas)
            { 
                CreateRoom(roomData);
            }

            return _levelGrid;
        }
        
        private List<RoomData> RandomizeRooms()
        {
            List<RoomData> pickedRooms = new List<RoomData>();
            List<RoomData> remainRooms = new List<RoomData>(_allRooms);

            for (int i = 0; i < 3; i++)
            {
                _excludeNextDoorAlignment = _lastPickedDoor switch
                {
                    DoorAlignment.Undefined => DoorAlignment.Undefined,
                    DoorAlignment.Left => DoorAlignment.Right,
                    DoorAlignment.Up => DoorAlignment.Down,
                    DoorAlignment.Down => DoorAlignment.Up,
                    DoorAlignment.Right => DoorAlignment.Left,
                    _ => _excludeNextDoorAlignment
                };

                List<RoomData> suitableRooms = new List<RoomData>(remainRooms.Where(room => room.DoorAlignment != _excludeNextDoorAlignment).ToList());

                var rnd = Random.Range(0, suitableRooms.Count);

                pickedRooms.Add(suitableRooms[rnd]);
                _lastPickedDoor = suitableRooms[rnd].DoorAlignment;
                remainRooms.Remove(suitableRooms[rnd]);
            }

            return pickedRooms;
        }

        private void CreateRoom(RoomData roomData)
        {
            Debug.LogError($"Create Room: {roomData.name}");

            if (_isLastLeft)
            {
                _nextStartPos.x -= roomData.GridSize.x;
                _isLastLeft = false;
            }
            
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
                _nextStartPos.x = _doorPosition.x;
                _nextStartPos.y = _doorPosition.y;
                _isLastLeft = true;
            }
        }

        private Vector2Int GetGridSize()
        {
            var summCols = 0;
            var summRows = 0;
            
            for (int i = 0; i < _pickedRoomDatas.Count; i++)
            {
                summCols += _pickedRoomDatas[i].GridSize.y;
                summRows += _pickedRoomDatas[i].GridSize.x;
            }
            
            return new Vector2Int(summRows, summCols);
        }
    }
}