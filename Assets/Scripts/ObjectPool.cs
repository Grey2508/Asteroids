using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int Count = 8;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private ObjectPool NextPool;

    private List<IPoolable> _objects = new List<IPoolable>();
    private int _currentIndex = 0;

    void Start()
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject newObject = Instantiate(Prefab, Vector3.zero, Quaternion.identity, transform);
            IPoolable newItem = newObject.GetComponent<IPoolable>();
            newItem.SetNextPool(NextPool);
            _objects.Add(newItem);
        }
    }

    public IPoolable GetNextObject()
    {
        while (true)
        {
            IPoolable result = _objects[_currentIndex++];

            if (_currentIndex >= Count)
                _currentIndex = 0;

            if (_objects[_currentIndex].Free)
            {
                return result;
            }
        }
    }

    public void Restart()
    {
        foreach(IPoolable item in _objects)
        {
            item.Free = true;
            item.SetActive(false);
        }

        _currentIndex = 0;
    }
}
