using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicWeapon", menuName = "ScriptableObjects/WeaponSO", order = 1)]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private float timeToLive = 5f;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float fireCooldown = 0.2f; // (second / bullets)

    public float TimeToLive => timeToLive;
    public float BulletSpeed => bulletSpeed;
    public float Damage => damage;
    public float FireCooldown => fireCooldown;
}
