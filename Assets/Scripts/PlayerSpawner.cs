using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    public GameObject InstantiatePlayerPrefab(Vector3 playerPosition)
    {
        return Instantiate(playerPrefab, playerPosition, Quaternion.identity);
    }

    public GameObject InstantiatePlayerPrefab()
    {
        return InstantiatePlayerPrefab(transform.position);
    }
}
