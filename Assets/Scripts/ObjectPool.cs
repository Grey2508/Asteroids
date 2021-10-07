using System;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int Count = 8;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private ObjectPool NextPool;

    private IPoolable[] _objects;
    private int _currentIndex = 0;

    void Awake()
    {
        _objects = new IPoolable[Count];

        FillPool(Count);

        //for (int i = 0; i < Count; i++)
        //{
        //    GameObject newObject = Instantiate(Prefab, Vector3.zero, Quaternion.identity, transform);
        //    IPoolable newItem = newObject.GetComponent<IPoolable>();
        //    newItem.SetNextPool(NextPool);
        //    newItem.Free = true;
        //    newItem.SetActive(false);
        //    _objects[i] = newItem;
        //}
    }

    public IPoolable GetNextObject()
    {
        int counter = 0;

        while (counter++ < 3)
        {
            IPoolable result = _objects[_currentIndex++];

            if (_currentIndex >= Count)
                _currentIndex = 0;

            if (_objects[_currentIndex].Free)
            {
                return result;
            }
        }

        MultiplyPool();

        return GetNextObject();
    }

    private void MultiplyPool()
    {
        Array.Resize(ref _objects, Count * 2);

        FillPool(Count * 2, Count);

        //for (int i = Count; i < Count * 2; i++)
        //{
        //    GameObject newObject = Instantiate(Prefab, Vector3.zero, Quaternion.identity, transform);
        //    IPoolable newItem = newObject.GetComponent<IPoolable>();
        //    newItem.SetNextPool(NextPool);
        //    _objects[i] = newItem;
        //}

        Count *= 2;
    }

    private void FillPool(int count, int startIndex = 0)
    {
        for (int i = startIndex; i < count; i++)
        {
            GameObject newObject = Instantiate(Prefab, Vector3.zero, Quaternion.identity, transform);
            IPoolable newItem = newObject.GetComponent<IPoolable>();
            newItem.SetNextPool(NextPool);
            newItem.Free = true;
            //newItem.SetActive(false);
            _objects[i] = newItem;
        }
    }

    public void Restart()
    {
        foreach (IPoolable item in _objects)
        {
            item.Free = true;
            item.SetActive(false);
        }

        _currentIndex = 0;
    }
}
