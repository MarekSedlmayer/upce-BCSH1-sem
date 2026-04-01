using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InUseButton : MonoBehaviour
{
    private InventoryUI _inventoryUI;
    private IWeapon _weapon = null;
    private Button _button;
    private TextMeshProUGUI _textObject;
    public bool IsNonActive => _weapon == null;

    public void Init(InventoryUI inventoryUI)
    {
        _inventoryUI = inventoryUI;
        _button = GetComponent<Button>();
        _textObject = GetComponentInChildren<TextMeshProUGUI>();
        _button.onClick.AddListener(OnClick);
        _button.interactable = false;
        _textObject.text = "";
    }

    public void Activate(IWeapon weapon)
    {
        _weapon = weapon;
        _button.interactable = true;
        _textObject.text = _weapon.GetWeaponData().ID;
    }

    public void OnClick()
    {
        if (_inventoryUI.TryDeactivateWeapon(_weapon))
        {
            _button.interactable = false;
            _textObject.text = "";
            _inventoryUI.CreateInventoryButton(_weapon);
            _weapon = null;
        }
    }
}
