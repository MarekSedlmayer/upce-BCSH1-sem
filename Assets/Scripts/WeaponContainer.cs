using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    private Weapon _weapon;

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void Shoot()
    {
        if (_weapon != null)
        {
            _weapon.TryShoot(transform.position, transform.rotation);
        }
    }
}
