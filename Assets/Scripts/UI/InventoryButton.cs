using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    private InventoryUI _inventoryUI;
    private IWeapon _weapon;
    private Button _button;
    private TextMeshProUGUI _textObject;

    public void Init(IWeapon weapon, InventoryUI inventoryUI)
    {
        _weapon = weapon;
        _inventoryUI = inventoryUI;
        _button = GetComponent<Button>();
        _textObject = GetComponentInChildren<TextMeshProUGUI>();
        _button.onClick.AddListener(OnClick);
        _textObject.text = _weapon.GetWeaponData().ID;
    }

    public void OnClick()
    {
        if (_inventoryUI.TryActivateWeapon(_weapon))
        {
            _inventoryUI.UpdateInUseButtons(_weapon);
            Destroy(gameObject);
        }
    }
}
