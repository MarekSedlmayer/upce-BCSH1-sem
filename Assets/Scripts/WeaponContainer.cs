using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{
    private IWeapon _weapon;

    public void SetWeapon(IWeapon weapon)
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
