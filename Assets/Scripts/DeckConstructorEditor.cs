using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColumnDummy))]
public class DeckConstructorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ColumnDummy column = (ColumnDummy)target;

        if (GUILayout.Button("Adjust Child Y Position"))
        {
            AdjustChildYPosition(column);
        }
    }
    
    private void AdjustChildYPosition(ColumnDummy column)
    {
        float _lastPos = 0f;
        
        foreach (Transform child in column.transform)
        {
            Vector3 newPosition = child.position;
            newPosition.y = _lastPos;
            child.position = newPosition;
            
            EditorUtility.SetDirty(child.gameObject);
            _lastPos += column._offsetY;
        }
    }
}
