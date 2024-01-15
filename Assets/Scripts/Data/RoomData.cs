using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "RoomData", order = 0)]
    public class RoomData : SerializedScriptableObject
    {
        [PropertyOrder(0)] 
        [SerializeField] private Vector2Int _grid = new Vector2Int(7, 7);
        [SerializeField] private DoorAlignment _doorAlignment;
        
        [PropertyOrder(3), TableMatrix(SquareCells = true, DrawElementMethod = "DrawLevelCard", Transpose = true), OnValueChanged("SetDoorAlignment")]
        [SerializeField] private LevelCardType[,] _levelCards;
        
        [PropertyOrder(1), Button]
        private void RegenerateData() => _levelCards = new LevelCardType[_grid.y, _grid.x];

        [ShowInInspector, Space(10),PropertyOrder(2), EnumToggleButtons, HideLabel, OnValueChanged("DeletePreviousDoor")]
        private LevelCardType _brushCardType;

        public Vector2Int GridSize => _grid;
        public DoorAlignment DoorAlignment => _doorAlignment;
        
        private readonly Dictionary<LevelCardType, Func<Color>> _colorMappings = new Dictionary<LevelCardType, Func<Color>>
            {
                {LevelCardType.Block, () => new Color(0, 0, 0, 0.5f)},
                {LevelCardType.Door, () => new Color(0.36f, 0.2f, 0.7f, 0.5f)},
                {LevelCardType.Empty, () => Color.gray},
                {LevelCardType.Enemy, () => Color.red},
                {LevelCardType.Random, () => Color.green},
                {LevelCardType.Unreachable, () => Color.clear},
            };

        public LevelCardType[,] GetCards()
        {
            LevelCardType[,] reversedArray = ReverseArray(_levelCards);
            return reversedArray;
        }
        
        LevelCardType[,] ReverseArray(LevelCardType[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);
            LevelCardType[,] reversedArray = new LevelCardType[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    reversedArray[i, j] = array[rows - 1 - i, j];
                }
            }

            return reversedArray;
        }

        
#if UNITY_EDITOR
        private LevelCardType DrawLevelCard(Rect rect, LevelCardType value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value = _brushCardType;
                GUI.changed = true;
                Event.current.Use();
            }

            UnityEditor.EditorGUI.DrawRect(rect.Padding(2), GetCardColor(value));

            return value;
        }

        private void SetDoorAlignment()
        {
            if (_brushCardType == LevelCardType.Door)
            {
               _doorAlignment = FindDoorAlignment();
               _brushCardType = LevelCardType.Empty;
            }
        }

        private DoorAlignment FindDoorAlignment()
        {
            for (int i = 0; i < _levelCards.GetLength(0); i++)
            {
                for (int j = 0; j < _levelCards.GetLength(1); j++)
                {
                    if (_levelCards[i, j] == LevelCardType.Door)
                    {
                        return ClaculateAlignment(i, j);
                    }
                }
            }

            return DoorAlignment.Undefined;
        }

        private DoorAlignment ClaculateAlignment(int col, int row)
        {
            if (col == _levelCards.GetLength(0) - 1)
            {
                return DoorAlignment.Down;
            }
            
            if (col == 0)
            {
                return DoorAlignment.Up;
            }

            if (row > _levelCards.GetLength(1) / 2)
            {
                return DoorAlignment.Right;
            }

            if (row < _levelCards.GetLength(1) / 2)
            {
                return DoorAlignment.Left;
            }

            return DoorAlignment.Undefined;
        }

        private void DeletePreviousDoor()
        {
            if (_brushCardType != LevelCardType.Door) return;
            
            for (int i = 0; i < _levelCards.GetLength(0); i++)
            {
                for (int j = 0; j < _levelCards.GetLength(1); j++)
                {
                    if (_levelCards[i, j] == LevelCardType.Door)
                    {
                        _levelCards[i, j] = LevelCardType.Empty;
                    }
                }
            }
        }

        public Color GetCardColor(LevelCardType value)
        {
            return _colorMappings.TryGetValue(value, out var getColor) ? getColor() : Color.yellow;
        }
#endif
    }
}