using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : IWeapon
{
    private readonly ObjectPool _bulletPool;
    private readonly WeaponScriptableObject _weaponData;
    public WeaponScriptableObject GetWeaponData() => _weaponData;

    public Shotgun(ObjectPool bulletPool, WeaponScriptableObject weaponData)
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

    private readonly int _numberOfPellets = 4;
    private readonly float _maxAngle = 20f;
    public void Shoot(Vector3 position, Quaternion rotation)
    {
        
        for (int i = 0; i < _numberOfPellets; i++)
        {
            GameObject obj = _bulletPool.Get();

            float t = _numberOfPellets == 1 ? 0.5f : i / (_numberOfPellets - 1f);
            float angle = Mathf.Lerp(-_maxAngle, _maxAngle, t);
            angle += Random.Range(-2f, 2f);
            Quaternion spreadRotation = rotation * Quaternion.Euler(0, 0, angle);

            if (obj.TryGetComponent<Bullet>(out var bullet))
            {
                bullet.Init(_weaponData.TimeToLive, _weaponData.BulletSpeed, _weaponData.Damage / _numberOfPellets);
            }
            obj.transform.SetPositionAndRotation(position, spreadRotation);
            obj.SetActive(true);
        }
    }
}
