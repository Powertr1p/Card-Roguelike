using Cards;
using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T : Card
{
    [SerializeField] private T _prefab;

    private readonly Vector3 _initialRotation = new Vector3(0, 180f, 0);
    
    public virtual T CreateNewInstance(int col, int row, Vector2 position, Vector2 offset, Transform parent)
    {
        var instance = Instantiate(_prefab, parent);
        instance.InitializePosition(new Vector2Int(row, col));
        instance.transform.position = new Vector3(position.x, position.y);
        instance.transform.rotation = Quaternion.Euler(_initialRotation);

        return instance;
    }

    protected virtual T CreateNewInstance()
    {
        return Instantiate(_prefab);
    }
}
