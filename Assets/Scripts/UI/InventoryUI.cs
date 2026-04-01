using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory UI list")]
    [SerializeField] private GameObject invButtonPrefab;
    [SerializeField] private GameObject invListParent;
    [Header("InUse/Active weapons UI list")]
    [SerializeField] private GameObject inUseButtonPrefab;
    [SerializeField] private GameObject inUseListParent;

    private InUseButton[] _inUseButtons;

    private Player _player;
    public void Init(Player player)
    {
        _player = player;
        CreateInUseButtons(player.WeaponContainers.Length);

        for (int i = 0; i < _inUseButtons.Length; i++)
        {
            if (player.WeaponContainers[i].Weapon != null)
            {
                UpdateInUseButtons(player.WeaponContainers[i].Weapon);
            }
        }
        foreach (var weapon in player.Inventory)
        {
            CreateInventoryButton(weapon);
        }
    }

    public bool TryActivateWeapon(IWeapon weapon)
    {
        return _player.TryActivateWeapon(weapon);
    }

    public bool TryDeactivateWeapon(IWeapon weapon)
    {
        return _player.TryDeactivateWeapon(weapon);
    }

    public void CreateInventoryButton(IWeapon weapon)
    {
        GameObject buttonObject = Instantiate(invButtonPrefab, invListParent.transform);
        InventoryButton invButton = buttonObject.GetComponent<InventoryButton>();
        invButton.Init(weapon, this);
    }

    public void UpdateInUseButtons(IWeapon weapon)
    {
        for (int i = 0; i < _inUseButtons.Length; i++)
        {
            if (_inUseButtons[i].IsNonActive)
            {
                _inUseButtons[i].Activate(weapon);
                return;
            }
        }
    }

    public void CreateInUseButtons(int numberOfButtons)
    {
        _inUseButtons = new InUseButton[numberOfButtons];
        for (int i = 0; i < numberOfButtons; i++)
        {
            GameObject buttonObject = Instantiate(inUseButtonPrefab, inUseListParent.transform);
            InUseButton inUseButton = buttonObject.GetComponent<InUseButton>();
            inUseButton.Init(this);
            _inUseButtons[i] = inUseButton;
        }
    }
}
