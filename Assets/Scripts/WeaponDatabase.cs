using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    [SerializeField] private List<WeaponScriptableObject> weaponSOs;
    private Dictionary<string, WeaponScriptableObject> _dictionary;
    void Awake()
    {
        _dictionary = weaponSOs.ToDictionary(w => w.ID, w => w);
    }

    public WeaponScriptableObject Get(string id)
    {
        return _dictionary[id];
    }

    public List<WeaponScriptableObject> GetAll()
    {
        return weaponSOs;
    }
}
