using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    public void Shoot()
    {
        if (weapon != null)
        {
            weapon.Shoot();
        }
    }
}
