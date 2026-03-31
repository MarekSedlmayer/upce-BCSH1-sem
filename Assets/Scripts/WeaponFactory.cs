using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponFactory
{
    public IWeapon Create(ObjectPool pool, WeaponScriptableObject wso)
    {
        return wso.WeaponType switch
        {
            WeaponType.Minigun => new Minigun(pool, wso),
            WeaponType.Shotgun => throw new NotImplementedException(),
            WeaponType.RocketLauncher => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }
}
