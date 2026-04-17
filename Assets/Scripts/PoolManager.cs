using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private WeaponDatabase weaponDatabase;
    [SerializeField] private List<ObjectPoolData> particlePools;
    private Dictionary<string, ObjectPool> _dictionary;

    void Awake()
    {
        _dictionary = new Dictionary<string, ObjectPool>();

        foreach (var weaponSO in weaponDatabase.GetAll())
        {
            GameObject child = new GameObject("BulletPool_" + weaponSO.ID);
            child.transform.parent = transform;

            ObjectPool pool = child.AddComponent<ObjectPool>();
            pool.Init(weaponSO.BulletPrefab, weaponSO.BulletPoolInitialSize);

            _dictionary.Add(weaponSO.ID, pool);
        }
        foreach (var poolData in particlePools)
        {
            GameObject child = new GameObject("ParticlePool_" + poolData.prefab.name);
            child.transform.parent = transform;

            ObjectPool pool = child.AddComponent<ObjectPool>();
            pool.Init(poolData.prefab, poolData.initSize);

            _dictionary.Add(poolData.prefab.name, pool);
        }
    }

    public ObjectPool GetPool(string id)
    {
        return _dictionary[id];
    }

    [Serializable]
    public struct ObjectPoolData
    {
        public GameObject prefab;
        public int initSize;
    }
}
