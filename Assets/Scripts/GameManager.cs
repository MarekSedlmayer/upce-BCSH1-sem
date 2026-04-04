using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WeaponDatabase weaponDatabase;
    [SerializeField] private RoomDatabase roomDatabase;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private WeaponScriptableObject[] startingWeapons = new WeaponScriptableObject[4];
    [SerializeField] private GameObject uiPauseMenu;
    [SerializeField] private GameObject uiHealthBar;
    [SerializeField] private InventoryUI inventoryUI;

    [SerializeField] private RoomScriptableObject startingRoom;

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
        uiPauseMenu.SetActive(true);
        _isGamePaused = true;
    }
    public void ResumeGame(Player player)
    {
        Time.timeScale = 1;
        player.EnableGameplayControls();
        uiPauseMenu.SetActive(false);
        _isGamePaused = false;
    }

    void Start()
    {
        CreatePlayerFromProfile();
    }

    private void CreatePlayerFromProfile()
    {
        Player playerScript;
        if (ProfileManager.Profile != null)
        {
            _activePlayerObject = playerSpawner.InstantiatePlayerPrefab(ProfileManager.Profile.PlayerPosition);
            playerScript = _activePlayerObject.GetComponentInChildren<Player>();

            roomDatabase.SetEmptyRooms(ProfileManager.Profile.clearedRooms);
            LoadWeapons(playerScript);
            if (!string.IsNullOrEmpty(ProfileManager.Profile.lastVisitedRoomID))
            {
                roomDatabase.Get(ProfileManager.Profile.lastVisitedRoomID).PlayerEntered();
            }
            else
            {
                roomDatabase.Get(startingRoom.ID).PlayerEntered();
            }
        }
        else // Debug only
        {
            _activePlayerObject = playerSpawner.InstantiatePlayerPrefab();
            playerScript = _activePlayerObject.GetComponentInChildren<Player>();

            LoadStartingWeapons(playerScript);
        }
        inventoryUI.Init(playerScript);
        playerScript.GamePaused += OnGamePaused;
        playerScript.PlayerDestroyed += OnPlayerDestroyed;
        playerScript.SetHealthBarRef(uiHealthBar);
    }

    private void OnPlayerDestroyed(Player playerScript)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadWeapons(Player playerScript)
    {
        if (ProfileManager.Profile.FirstTime)
        {
            LoadStartingWeapons(playerScript);
            return;
        }

        foreach (string weaponId in ProfileManager.Profile.Inventory)
        {
            var pool = poolManager.GetPool(weaponId);
            var wSO = weaponDatabase.Get(weaponId);
            playerScript.Inventory.Add(_weaponFactory.Create(pool, wSO));
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
        for (int i = 0; i < startingWeapons.Length; i++)
        {
            if (startingWeapons[i] != null)
                playerScript.WeaponContainers[i].Weapon = _weaponFactory.Create(poolManager.GetPool(startingWeapons[i].ID), startingWeapons[i]);
        }
    }

    public void SavePlayerStateAndQuit()
    {
        Player playerScript = _activePlayerObject.GetComponentInChildren<Player>();
        if (ProfileManager.Profile != null)
        {
            ProfileManager.Profile.PlayerPosition = _activePlayerObject.transform.position;
            ProfileManager.Profile.WeaponsInUse = playerScript.WeaponContainers.Select(w => w.Weapon != null ? w.Weapon.GetWeaponData().ID : "").ToArray();
            ProfileManager.Profile.FirstTime = false;

            ProfileManager.Profile.Inventory = playerScript.Inventory.Select(w => w.GetWeaponData().ID).ToList();

            var items = roomDatabase.GetEmptyRoomIDsAndLastVisitedRoomID();
            ProfileManager.Profile.clearedRooms = items.Item1;
            ProfileManager.Profile.lastVisitedRoomID = items.Item2;

            ProfileManager.SaveActiveProfile();
        }
        ResumeGame(playerScript);
        SceneManager.LoadScene("MainMenu");
    }
}
