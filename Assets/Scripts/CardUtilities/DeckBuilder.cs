using System.Collections.Generic;
using System.Linq;
using Data;
using DeckMaster;
using UnityEngine;

namespace CardUtilities
{
    public class DeckBuilder
    {
        private List<RoomData> _pickedRoomDatas;
        private List<RoomData> _allRooms;
        private List<RoomData> _remainRooms;
        
        private CardData[,] _levelGrid;
        private DoorAlignment _lastPickedDoor = DoorAlignment.Undefined;
        private DoorAlignment _excludeNextDoorAlignment = DoorAlignment.Undefined;

        private Vector2Int _gridSize;
        private Vector2Int _initialGridSize = new(200, 200);
        private Vector2Int _nextStartPos;
        private Vector2Int _doorPosition = Vector2Int.zero;
        
        private int _maxRooms;
        private bool _isLastLeft = false;
        private bool _isLastDown = false;
        private bool _isLastUp;

        public DeckBuilder(List<RoomData> allRooms, int maxRooms)
        {
            _allRooms = allRooms;
            _maxRooms = maxRooms;
            
            _nextStartPos = _initialGridSize / 2;
            
            _pickedRoomDatas = RandomizeRooms();
            _gridSize = GetGridSize();
        }

        public CardData[,] GetConcatinatedRooms()
        {
            _levelGrid = new CardData[_initialGridSize.y, _initialGridSize.x];

            for (int i = 0; i < _levelGrid.GetLength(0); i++)
            {
                for (int j = 0; j < _levelGrid.GetLength(1); j++)
                {
                    _levelGrid[i, j] = new CardData();
                }
            }

            int roomNumber = 1;
            int cycle = 0;

            while (roomNumber <= GameRulesGetter.Rules.MaxRooms && cycle < 50)
            {
                if (CanPositionRoom(_pickedRoomDatas[roomNumber - 1]))
                {
                    CreateRoom(_pickedRoomDatas[roomNumber - 1], roomNumber);
                    roomNumber++;
                }
                else
                {
                    ReplaceRoomWithNewOne(_pickedRoomDatas[roomNumber - 1]);
                }
                
                cycle++;

                if (cycle == 49)
                {
                    Debug.LogError("ERROR! TOO MANY CYCLES > 50");
                }
            }

            return ConvertGridToProperSize();
        }


        //     foreach (var roomData in _pickedRoomDatas)
        //     {
        //         if (CanPositionRoom(roomData))
        //         {
        //             CreateRoom(roomData, roomNumber);
        //             roomNumber++;
        //         }
        //         else
        //         {
        //             ReplaceRoomWithNewOne(roomData);
        //         }
        //     }
        //
        //     return ConvertGridToProperSize();
        // }
        
        private List<RoomData> RandomizeRooms()
        {
            List<RoomData> pickedRooms = new List<RoomData>();
            _remainRooms = new List<RoomData>(_allRooms);

            for (int i = 0; i < _maxRooms; i++)
            {
                SetExcludeDoorAlignment();

                List<RoomData> suitableRooms = GetSuitableRooms(i, _remainRooms);
                
                var rnd = Random.Range(0, suitableRooms.Count);

                pickedRooms.Add(suitableRooms[rnd]);
                _lastPickedDoor = suitableRooms[rnd].DoorAlignment;
                _remainRooms.Remove(suitableRooms[rnd]);
            }

            return pickedRooms;
        }

        private void ReplaceRoomWithNewOne(RoomData data)
        {
            int index = _pickedRoomDatas.IndexOf(data);

            Debug.Log($"Starting replacing! Current index of replacing room is {index}, its: {data.name}");
            
            _lastPickedDoor = _pickedRoomDatas[index - 1].DoorAlignment;

            SetExcludeDoorAlignment();
            List<RoomData> suitableRooms = new List<RoomData>(_remainRooms.Where(room => room.DoorAlignment != _excludeNextDoorAlignment).ToList());

            var rnd = Random.Range(0, suitableRooms.Count);

            if (suitableRooms.Count == 0)
            {
                _remainRooms = new List<RoomData>(_allRooms.Except(_pickedRoomDatas));
                return;
            }
            
            _pickedRoomDatas[index] = suitableRooms[rnd];
            _remainRooms.Remove(suitableRooms[rnd]);
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

        private bool CanPositionRoom(RoomData roomData)
        {
            Debug.Log($"Trying to position room {roomData.name}");
            
            var roomCards = roomData.GetCards();
            Vector2Int nextStartPos =  CorrectStartPosition(_nextStartPos, roomData);

            for (int i = 0; i < roomCards.GetLength(0); i++) 
            {
                for (var j = 0; j < roomCards.GetLength(1); j++)
                {
                    var currentColumn = nextStartPos.y + i;
                    var currentRow = nextStartPos.x + j;

                    var type = _levelGrid[currentColumn, currentRow].Type;
                    
                    if (type != LevelCardType.Unreachable)
                    {
                        Debug.Log($"- Positioning room {roomData.name} is FAILED!!");
                        return false;
                    }
                }
            }

            Debug.Log($"+ Positioning room {roomData.name} is succeed!");
            
            return true;
        }
        
        private Vector2Int CorrectStartPosition(Vector2Int nextStartPos, RoomData data)
        {
            if (!_isLastDown)
            {
                if (data.DoorAlignment == DoorAlignment.Down)
                {
                    nextStartPos.y -= (data.GridSize.y - 1);
                }
            }
            
            if (_isLastLeft)
            {
                nextStartPos.x -= data.GridSize.x;
            }
            else if (_isLastDown)
            {
                nextStartPos.y -= (data.GridSize.y - 1);
            }
            else if (_isLastUp)
            {
                var offset = data.GridSize.x / 2;
                nextStartPos.x -= offset;
            }

            return nextStartPos;
        }

        private void CreateRoom(RoomData roomData, int roomNumber)
        {
           //Debug.LogError($"Create Room: {roomData.name}");
           var roomCards = roomData.GetCards();

           CorrectStartPosition(roomData);
           
           for (int i = 0; i < roomCards.GetLength(0); i++) 
           {
               for (var j = 0; j < roomCards.GetLength(1); j++)
               {
                   var currentColumn = _nextStartPos.y + i;
                   var currentRow = _nextStartPos.x + j;

                   //Debug.Log($"{currentColumn} {currentRow}");
                   
                   if (roomCards[i, j] == LevelCardType.Random)
                   {
                      roomCards[i, j] = GetRandomCard();
                   }

                   _levelGrid[currentColumn, currentRow] = new CardData(roomNumber, roomCards[i, j], new Vector2Int(currentColumn, currentRow));

                   if (roomCards[i, j] == LevelCardType.Door)
                   {
                       _doorPosition.x = currentRow;
                       _doorPosition.y = currentColumn;
                   }
               }
           }

           SetNextStartPosition(roomData);
        }

        private void CorrectStartPosition(RoomData roomData)
        {
            if (!_isLastDown)
            {
                if (roomData.DoorAlignment == DoorAlignment.Down)
                {
                    _nextStartPos.y -= (roomData.GridSize.y - 1);
                }
            }
            
            if (_isLastLeft)
            {
                _nextStartPos.x -= roomData.GridSize.x;
                _isLastLeft = false;
            }
            else if (_isLastDown)
            {
                _nextStartPos.y -= (roomData.GridSize.y - 1);
                _isLastDown = false;
            }
            else if (_isLastUp)
            {
                var offset = roomData.GridSize.x / 2;
                _nextStartPos.x -= offset;
                _isLastUp = false;
            }
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
                _isLastUp = true;
            }
            else if (roomData.DoorAlignment == DoorAlignment.Left)
            {
                _nextStartPos.x = _doorPosition.x;
                _nextStartPos.y = _doorPosition.y;
                _isLastLeft = true;
            }
            else if (roomData.DoorAlignment == DoorAlignment.Down)
            {
                _nextStartPos.x = _doorPosition.x;
                _nextStartPos.y = _doorPosition.y - 1;
                _isLastDown = true;
            }
        }

        private CardData[,] ConvertGridToProperSize()
        {
            Vector2Int startPos = GetFirstNonEmptyGridCells();
           
            int startedCol = startPos.y;
            int startedRow = startPos.x;

            Vector2Int size = new Vector2Int(_gridSize.y, _gridSize.x);
            
           // Debug.Log($"LastCol: {lastCol}, StartedCol: {startedCol}; LastRow: {lastRow}, StartedRow: {startedRow}");
            
           CardData[,] newArray = new CardData[size.x, size.y];
            
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

        private Vector2Int GetFirstNonEmptyGridCells()
        {
            int startedCol = _initialGridSize.y / 2;
            int startedRow = _initialGridSize.x / 2;

            for (int i = 0; i < _levelGrid.GetLength(1); i++)
            {
                for (int j = 0; j < _levelGrid.GetLength(0); j++)
                {
                    if (_levelGrid[j, i].Type != LevelCardType.Unreachable)
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

            return new Vector2Int(startedRow, startedCol);
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

        private LevelCardType GetRandomCard()
        {
            float randomValue = Random.value;
            
            Debug.Log($"Random Value is: {randomValue}");

            var enemyChance = GameRulesGetter.Rules.EnemySpawnChance;
            var itemChance = enemyChance + GameRulesGetter.Rules.ItemSpawnChance;
            var emptyChance = itemChance + GameRulesGetter.Rules.EmptySpawnChance;

            if (randomValue < enemyChance)
            {
                return LevelCardType.Enemy;
            }
            else if (randomValue < itemChance)
            {
                return LevelCardType.Item;
            }
            else if (randomValue < emptyChance)
            {
                return LevelCardType.Empty;
            }
            else
            {
                return LevelCardType.Block;
            }
        }
    }
}