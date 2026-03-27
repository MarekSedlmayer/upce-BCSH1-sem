using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ProfileData ProfileData;
    
    private WeaponDatabase _weaponDatabase;
    private PoolManager _poolManager;

    [SerializeField] private GameObject playerPrefab;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Mission_01")
        {
            _weaponDatabase = FindObjectOfType<WeaponDatabase>();
            _poolManager = FindObjectOfType<PoolManager>();

            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab, ProfileData.PlayerPosition, Quaternion.identity);
        Player playerScript = playerObject.GetComponentInChildren<Player>();

        if (ProfileData.FirstTime)
        {
            var wSO = _weaponDatabase.Get("BasicMinigun"); // TODO: Define starting weapons elsewhere
            playerScript.WeaponContainers[0].SetWeapon(new Weapon(_poolManager.GetPool(wSO.ID), wSO));
            playerScript.WeaponContainers[1].SetWeapon(new Weapon(_poolManager.GetPool(wSO.ID), wSO));
            return;
        }

        for (int i = 0; i < playerScript.WeaponContainers.Length; i++)
        {
            string weaponId = ProfileData.WeaponsInUse[i];
            if (!string.IsNullOrEmpty(weaponId))
            {
                var wSO = _weaponDatabase.Get(weaponId);
                playerScript.WeaponContainers[i].SetWeapon(new Weapon(_poolManager.GetPool(weaponId), wSO)); // TODO: WeaponFactory, WeaponType
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
