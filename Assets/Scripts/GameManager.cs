using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WeaponDatabase weaponDatabase;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private WeaponScriptableObject startingWeapon;
    [SerializeField] private GameObject uiPauseMenuCanvas;

    private WeaponFactory _weaponFactory = new WeaponFactory();
    private bool _isGamePaused = false;
    private void OnGamePaused(Player player)
    {
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused)
        {
            Time.timeScale = 0;
            player.EnablePauseMenuControls();
            uiPauseMenuCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            player.EnableGameplayControls();
            uiPauseMenuCanvas.SetActive(false);
        }
    }

    void Start()
    {
        Player playerScript;
        if (ProfileManager.Profile != null)
        {
            GameObject playerObject = playerSpawner.InstantiatePlayerPrefab(ProfileManager.Profile.PlayerPosition);
            playerScript = playerObject.GetComponentInChildren<Player>();

            LoadWeapons(playerScript);
        }
        else // Debug only
        {
            GameObject playerObject = playerSpawner.InstantiatePlayerPrefab();
            playerScript = playerObject.GetComponentInChildren<Player>();

            LoadStartingWeapons(playerScript);
        }
        playerScript.GamePaused += OnGamePaused;
    }

    private void LoadWeapons(Player playerScript)
    {
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
                playerScript.WeaponContainers[i].SetWeapon(_weaponFactory.Create(poolManager.GetPool(weaponId), wSO));
            }
        }
    }

    private void LoadStartingWeapons(Player playerScript)
    {
        playerScript.WeaponContainers[0].SetWeapon(_weaponFactory.Create(poolManager.GetPool(startingWeapon.ID), startingWeapon));
        playerScript.WeaponContainers[1].SetWeapon(_weaponFactory.Create(poolManager.GetPool(startingWeapon.ID), startingWeapon));
    }
}
