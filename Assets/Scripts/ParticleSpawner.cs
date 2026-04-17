using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
