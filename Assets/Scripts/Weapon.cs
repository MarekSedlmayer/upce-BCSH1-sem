using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;

    public void Shoot()
    {
        GameObject bullet = bulletPool.Get();
        
        bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
        bullet.SetActive(true);
    }
}
