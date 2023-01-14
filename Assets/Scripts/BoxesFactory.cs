using UnityEngine;
using UnityEngine.Pool;

public class BoxesFactory : MonoBehaviour
{
    [SerializeField] private GameObject _boxPrefab;

    private IObjectPool<GameObject> _boxesPool;

    private void Awake()
    {
        _boxesPool = new ObjectPool<GameObject>(createFunc: CreateBox);
    }

    private void OnValidate()
    {
        if (!_boxPrefab.TryGetComponent<Box>(out _))
            throw new System.Exception("Installed prefab is not a Box");
    }

    private GameObject CreateBox()
    {
        GameObject boxObject = Instantiate<GameObject>(_boxPrefab);
        Box box = boxObject.GetComponent<Box>();
        return boxObject;
    }

    public Box getBox()
    {
        GameObject boxObject = _boxesPool.Get();
        Box box = boxObject.GetComponent<Box>();
        box.Reset();
        return box;
    }

    public void freeBox(Box box)
    {
        _boxesPool.Release(box.gameObject);
    }
}
