using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private WeaponScriptableObject weaponData;

    private float _nextFireTime = 0f;

    public void TryShoot()
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = weaponData.FireCooldown + Time.time;
        }
    }

    public void Shoot()
    {
        GameObject obj = bulletPool.Get();

        if (obj.TryGetComponent<Bullet>(out var bullet))
        {
            bullet.Init(weaponData.TimeToLive, weaponData.BulletSpeed, weaponData.Damage);
        }
        obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
        obj.SetActive(true);
    }
}
