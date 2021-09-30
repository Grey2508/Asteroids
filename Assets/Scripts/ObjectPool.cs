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
            newObject.SetActive(false);

            IPoolable newItem = newObject.GetComponent<IPoolable>();
            newItem.Free = true;
            newItem.SetAsteroidPool(NextPool);

            _objects.Add(newItem);
        }
    }

    public IPoolable GetNextObject()
    {
        while (!_objects[++_currentIndex].Free)
        {
            if (_currentIndex == Count)
                _currentIndex = 0;

            _objects[_currentIndex].Free = false;

            return _objects[_currentIndex];
        }

        throw new System.ArgumentOutOfRangeException("Недостаточно элементов в пуле");
    }
}
