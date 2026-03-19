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

    public float TimeToLive { get => timeToLive; private set => timeToLive = value; }
    public float BulletSpeed { get => bulletSpeed; private set => bulletSpeed = value; }
    public float Damage { get => damage; private set => damage = value; }
    public float FireCooldown { get => fireCooldown; private set => fireCooldown = value; }
}
