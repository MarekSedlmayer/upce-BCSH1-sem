using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private float timeToLive = 5f;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float damage = 1f;
    public void Shoot()
    {
        GameObject obj = bulletPool.Get();

        if (obj.TryGetComponent<Bullet>(out var bullet))
        {
            bullet.SetTimeToLive(timeToLive);
            bullet.SetSpeed(bulletSpeed);
            bullet.SetDamage(damage);
        }
        obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
        obj.SetActive(true);
    }
}
