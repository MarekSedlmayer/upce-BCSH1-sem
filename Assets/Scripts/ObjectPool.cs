using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject _prefab;
    private int _initialSize;

    private readonly List<GameObject> _pool = new List<GameObject>();

    public void Init(GameObject prefab, int initialSize)
    {
        _prefab = prefab;
        _initialSize = initialSize;

        for (int i = 0; i < _initialSize; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(_prefab, transform);
        obj.SetActive(false);
        _pool.Add(obj);
        return obj;
    }
    public GameObject Get()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }
        return CreateObject();
    }
}
