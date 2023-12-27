using System.Collections.Generic;
using Data;
using UnityEngine;

namespace CardUtilities
{
    public class DeckBuilder
    {
        private List<RoomData> _roomDatas;

        private Vector2Int _gridSize;
        
        public DeckBuilder(List<RoomData> roomDatas)
        {
            _roomDatas = roomDatas;
            _gridSize = GetGridSize();
        }

        public LevelCardType[,] GetConcatinatedRooms()
        {
            LevelCardType[,] concatinatedGrid = new LevelCardType[_gridSize.y, _gridSize.x];

            int concCols = 0;

            foreach (var roomData in _roomDatas)
            {
                var roomCards = roomData.GetCards();

                for (int i = 0; i < roomCards.GetLength(0); i++)
                {
                    for (int j = 0; j < roomCards.GetLength(1); j++)
                    {
                        concatinatedGrid[concCols, j] = roomCards[i, j];
                    }
                    
                    concCols++;
                }
            }

            return concatinatedGrid;
        }

        private Vector2Int GetGridSize()
        {
            var summCols = 0;
            var summRows = 0;
            
            for (int i = 0; i < _roomDatas.Count; i++)
            {
                summCols += _roomDatas[i].GridSize.y;
                summRows = _roomDatas[i].GridSize.x;
            }

            return new Vector2Int(summRows, summCols);
        }
    }
}