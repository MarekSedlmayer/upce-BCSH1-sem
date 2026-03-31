using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    public IWeapon Weapon;

    public void Shoot()
    {
        if (Weapon != null)
        {
            Weapon.TryShoot(transform.position, transform.rotation);
        }
    }
}
