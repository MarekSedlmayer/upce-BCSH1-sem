using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Shoot()
    {
        if (weapon != null)
        {
            weapon.Shoot();
        }
    }
}
