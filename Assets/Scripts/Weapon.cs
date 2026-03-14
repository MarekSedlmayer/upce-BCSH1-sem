using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private float timeToLive = 5f;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float fireCooldown = 0.2f; // (second / bullets)

    private float _nextFireTime = 0f;

    public void TryShoot()
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = fireCooldown + Time.time;
        }
    }

    public void Shoot()
    {
        GameObject obj = bulletPool.Get();

        if (obj.TryGetComponent<Bullet>(out var bullet))
        {
            bullet.Init(timeToLive, bulletSpeed, damage);
        }
        obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
        obj.SetActive(true);
    }
}
