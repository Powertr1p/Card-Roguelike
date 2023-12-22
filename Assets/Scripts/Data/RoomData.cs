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
        [SerializeField, PropertyOrder(0)] 
        private Vector2Int _grid = new Vector2Int(7, 7);
        
        [PropertyOrder(1), Button]
        private void RegenerateData() => _levelCards = new LevelCardType[_grid.x, _grid.y];

        [ShowInInspector, Space(10),PropertyOrder(2), EnumToggleButtons, HideLabel]
        private LevelCardType _brushCardType;
        
        [PropertyOrder(3), TableMatrix(SquareCells = true, DrawElementMethod = "DrawLevelCard", Transpose = true)]
        [SerializeField] private LevelCardType[,] _levelCards;
        
        public Vector2Int GridSize => _grid;

        private readonly Dictionary<LevelCardType, Func<Color>> _colorMappings = new Dictionary<LevelCardType, Func<Color>>
            {
                {LevelCardType.Block, () => new Color(0, 0, 0, 0.5f)},
                {LevelCardType.Door, () => new Color(0.36f, 0.2f, 0.7f, 0.5f)},
                {LevelCardType.Empty, () => Color.gray},
                {LevelCardType.Enemy, () => Color.red},
                {LevelCardType.Random, () => Color.green}
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
                    reversedArray[i, j] = array[rows - 1 - i, columns - 1 - j];
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
        
        public Color GetCardColor(LevelCardType value)
        {
            return _colorMappings.TryGetValue(value, out var getColor) ? getColor() : Color.yellow;
        }
#endif
    }
}