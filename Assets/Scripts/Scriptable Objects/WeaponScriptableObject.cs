using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicWeapon", menuName = "ScriptableObjects/WeaponSO", order = 1)]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private float timeToLive = 5f;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float fireCooldown = 0.2f; // (second / bullets)
    [SerializeField] private float maxRandomFireDelay = 0.1f;
    [SerializeField] private WeaponType weaponType = WeaponType.Minigun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletPoolInitialSize;

    public string ID => id;
    public float TimeToLive => timeToLive;
    public float BulletSpeed => bulletSpeed;
    public float Damage => damage;
    public float FireCooldown => fireCooldown;
    public float MaxRandomFireDelay => maxRandomFireDelay;
    public WeaponType WeaponType => weaponType;
    public GameObject BulletPrefab => bulletPrefab;
    public int BulletPoolInitialSize => bulletPoolInitialSize;
}
public enum WeaponType
{
    Minigun,
    Shotgun,
    RocketLauncher
}
