using System.Collections.Generic;
using Cards;
using DefaultNamespace.Effects;
using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T : Card
{
    [SerializeField] private T _prefab;

    public virtual T CreateNewInstance(int col, int row, int position, Vector2 offset)
    {
        var instance = Instantiate(_prefab);
        instance.Initialize(new Vector2Int(row, col));
        instance.transform.position = new Vector3(position, col * offset.y);

        return instance;
    }
}
