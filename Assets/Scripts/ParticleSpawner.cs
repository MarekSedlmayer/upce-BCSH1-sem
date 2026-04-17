using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private ObjectPool _pool;

    void Start()
    {
        _pool = FindObjectOfType<PoolManager>().GetPool(prefab.name);
    }

    public void Spawn()
    {
        var obj = _pool.Get();
        obj.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        obj.SetActive(true);
        obj.GetComponent<ParticleSystem>().Play();
    }
}
