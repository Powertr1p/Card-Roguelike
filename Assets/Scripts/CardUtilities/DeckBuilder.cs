using System.Collections.Generic;
using System.Linq;
using Data;
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
        private Vector2Int _initialGridSize = new(40, 40);
        private Vector2Int _nextStartPos;
        private Vector2Int _doorPosition = Vector2Int.zero;
        
        private int _maxRooms;
        
        private bool _isLastLeft = false;
        private bool _isLastDown = false;
        
        public DeckBuilder(List<RoomData> allRooms, int maxRooms)
        {
            _allRooms = allRooms;
            _maxRooms = maxRooms;
            
            _nextStartPos = _initialGridSize / 2;
            
            _pickedRoomDatas = RandomizeRooms();
            _gridSize = GetGridSize();
        }

        public LevelCardType[,] GetConcatinatedRooms()
        {
            _levelGrid = new LevelCardType[_initialGridSize.y, _initialGridSize.x];

            foreach (var roomData in _pickedRoomDatas)
            { 
                CreateRoom(roomData);
            }

            return ConvertGridToProperSize();
        }
        
        private List<RoomData> RandomizeRooms()
        {
            List<RoomData> pickedRooms = new List<RoomData>();
            List<RoomData> remainRooms = new List<RoomData>(_allRooms);

            for (int i = 0; i < _maxRooms; i++)
            {
                SetExcludeDoorAlignment();

                List<RoomData> suitableRooms = GetSuitableRooms(i, remainRooms);
                
                var rnd = Random.Range(0, suitableRooms.Count);

                pickedRooms.Add(suitableRooms[rnd]);
                _lastPickedDoor = suitableRooms[rnd].DoorAlignment;
                remainRooms.Remove(suitableRooms[rnd]);
            }

            return pickedRooms;
        }

        private List<RoomData> GetSuitableRooms(int roomIndex, List<RoomData> remainRooms)
        {
            return roomIndex == 0 ? 
                new List<RoomData>(remainRooms.Where(room => room.DoorAlignment == DoorAlignment.Up).ToList()) :
                new List<RoomData>(remainRooms.Where(room => room.DoorAlignment != _excludeNextDoorAlignment).ToList());
        }

        private void SetExcludeDoorAlignment()
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
        }

        private void CreateRoom(RoomData roomData)
        {
           //Debug.LogError($"Create Room: {roomData.name}");

           var roomCards = roomData.GetCards();
            
            for (int i = 0; i < roomCards.GetLength(0); i++)
            {
                for (int j = 0; j < roomCards.GetLength(1); j++)
                {
                    var currentColumn = _nextStartPos.y + i;
                    var currentRow = _nextStartPos.x + j;
                    
                    //Debug.Log($"{currentColumn} {currentRow}");
                    
                    _levelGrid[currentColumn, currentRow] = roomCards[i, j];

                    if (roomCards[i, j] == LevelCardType.Door)
                    {
                        _doorPosition.x = currentRow;
                        _doorPosition.y = currentColumn;
                    }
                }
            }

            SetNextStartPosition(roomData);
        }

        private void SetNextStartPosition(RoomData roomData)
        {
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
                _nextStartPos.x -= roomData.GridSize.x - 1;
                _nextStartPos.y = _doorPosition.y;
            }
            else if (roomData.DoorAlignment == DoorAlignment.Down)
            {
                _nextStartPos.x = _doorPosition.x;
                _nextStartPos.y = _doorPosition.y - 1;
                _nextStartPos.y -= roomData.GridSize.y;
            }
        }

        private LevelCardType[,] ConvertGridToProperSize()
        {
            int startedCol = 30;
            int startedRow = 30;

            for (int i = 0; i < _levelGrid.GetLength(1); i++)
            {
                for (int j = 0; j < _levelGrid.GetLength(0); j++)
                {
                    if (_levelGrid[j, i] != LevelCardType.Empty)
                    {
                        var currentCol = j;
                        var currentRow = i;
            
                        if (currentCol < startedCol)
                            startedCol = currentCol;

                        if (currentRow < startedRow)
                            startedRow = currentRow;
                    }
                }
            }
            
            Vector2Int size = new Vector2Int(_gridSize.y, _gridSize.x);
            
           // Debug.Log($"LastCol: {lastCol}, StartedCol: {startedCol}; LastRow: {lastRow}, StartedRow: {startedRow}");
            
            LevelCardType[,] newArray = new LevelCardType[size.x, size.y];
            
            for (int i = 0; i < newArray.GetLength(0); i++)
            {
                for (int j = 0; j < newArray.GetLength(1); j++)
                {
                    newArray[i, j] = _levelGrid[startedCol + i, startedRow + j];
                    
                   // Debug.Log($"col: {i}, row: {j}. Type: {newArray[i,j].ToString()}");
                }
            }
            
            return newArray;
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