using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WeaponDatabase weaponDatabase;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private WeaponScriptableObject startingWeapon;

    void Start()
    {
        if (ProfileManager.Profile != null)
        {
            GameObject playerObject = playerSpawner.InstantiatePlayerPrefab(ProfileManager.Profile.PlayerPosition);
            SpawnPlayer(playerObject);
        }
        else
        {
            GameObject playerObject = playerSpawner.InstantiatePlayerPrefab();
            LoadStartingWeapons(playerObject.GetComponentInChildren<Player>());
        }
    }

    private void SpawnPlayer(GameObject playerObject)
    {
        Player playerScript = playerObject.GetComponentInChildren<Player>();

        if (ProfileManager.Profile.FirstTime)
        {
            LoadStartingWeapons(playerScript);
            return;
        }

        for (int i = 0; i < playerScript.WeaponContainers.Length; i++)
        {
            string weaponId = ProfileManager.Profile.WeaponsInUse[i];
            if (!string.IsNullOrEmpty(weaponId))
            {
                var wSO = weaponDatabase.Get(weaponId);
                playerScript.WeaponContainers[i].SetWeapon(new Weapon(poolManager.GetPool(weaponId), wSO)); // TODO: WeaponFactory, WeaponType
            }
        }
    }

    private void LoadStartingWeapons(Player playerScript)
    {
        playerScript.WeaponContainers[0].SetWeapon(new Weapon(poolManager.GetPool(startingWeapon.ID), startingWeapon));
        playerScript.WeaponContainers[1].SetWeapon(new Weapon(poolManager.GetPool(startingWeapon.ID), startingWeapon));
    }
}
