using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private readonly ObjectPool _bulletPool;
    private readonly WeaponScriptableObject _weaponData;

    public Weapon(ObjectPool bulletPool, WeaponScriptableObject weaponData)
    {
        _bulletPool = bulletPool;
        _weaponData = weaponData;
    }

    private float _nextFireTime = 0f;

    public void TryShoot(Vector3 position, Quaternion rotation)
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot(position, rotation);
            _nextFireTime = _weaponData.FireCooldown + Time.time + (_weaponData.MaxRandomFireDelay * Random.value);
        }
    }

    public void Shoot(Vector3 position, Quaternion rotation)
    {
        GameObject obj = _bulletPool.Get();

        if (obj.TryGetComponent<Bullet>(out var bullet))
        {
            bullet.Init(_weaponData.TimeToLive, _weaponData.BulletSpeed, _weaponData.Damage);
        }
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
    }
}
