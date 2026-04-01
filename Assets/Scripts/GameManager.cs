using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WeaponDatabase weaponDatabase;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private WeaponScriptableObject startingWeapon;
    [SerializeField] private GameObject uiPauseMenuCanvas;
    [SerializeField] private InventoryUI inventoryUI;

    private GameObject _activePlayerObject;
    private readonly WeaponFactory _weaponFactory = new WeaponFactory();

    private bool _isGamePaused = false;
    private void OnGamePaused(Player player)
    {
        if (_isGamePaused) { ResumeGame(player); }
        else { PauseGame(player); }
    }
    public void PauseGame(Player player)
    {
        Time.timeScale = 0;
        player.EnablePauseMenuControls();
        uiPauseMenuCanvas.SetActive(true);
        _isGamePaused = true;
    }
    public void ResumeGame(Player player)
    {
        Time.timeScale = 1;
        player.EnableGameplayControls();
        uiPauseMenuCanvas.SetActive(false);
        _isGamePaused = false;
    }

    void Start()
    {
        Player playerScript;
        if (ProfileManager.Profile != null)
        {
            _activePlayerObject = playerSpawner.InstantiatePlayerPrefab(ProfileManager.Profile.PlayerPosition);
            playerScript = _activePlayerObject.GetComponentInChildren<Player>();

            LoadWeapons(playerScript);
        }
        else // Debug only
        {
            _activePlayerObject = playerSpawner.InstantiatePlayerPrefab();
            playerScript = _activePlayerObject.GetComponentInChildren<Player>();

            LoadStartingWeapons(playerScript);
        }
        inventoryUI.Init(playerScript);
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
                var pool = poolManager.GetPool(weaponId);
                var wSO = weaponDatabase.Get(weaponId);
                playerScript.WeaponContainers[i].Weapon = _weaponFactory.Create(pool, wSO);
            }
        }
    }

    private void LoadStartingWeapons(Player playerScript)
    {
        playerScript.WeaponContainers[0].Weapon = _weaponFactory.Create(poolManager.GetPool(startingWeapon.ID), startingWeapon);
        playerScript.WeaponContainers[1].Weapon = _weaponFactory.Create(poolManager.GetPool(startingWeapon.ID), startingWeapon);
    }

    public void SavePlayerStateAndQuit()
    {
        Player playerScript = _activePlayerObject.GetComponentInChildren<Player>();
        if (ProfileManager.Profile != null)
        {
            ProfileManager.Profile.PlayerPosition = _activePlayerObject.transform.position;
            ProfileManager.Profile.WeaponsInUse = playerScript.WeaponContainers.Select(w => w.Weapon != null ? w.Weapon.GetWeaponData().ID : "").ToArray();
            ProfileManager.Profile.FirstTime = false;

            ProfileManager.SaveActiveProfile();
        }
        ResumeGame(playerScript);
        SceneManager.LoadScene("MainMenu");
    }
}
