using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private WeaponContainer[] weaponContainers = new WeaponContainer[4];
    [SerializeField] private PlayerInput playerInput;
    public bool IsInEmptyRoom = true;
    public WeaponContainer[] WeaponContainers => weaponContainers;
    public event Action<Player> GamePaused;
    public event Action<Player> PlayerDestroyed;

    private InputAction _fireAction;

    private List<IWeapon> _inventory = new List<IWeapon>();
    public List<IWeapon> Inventory => _inventory;

    public bool TryActivateWeapon(IWeapon weapon)
    {
        for (int i = 0; i < weaponContainers.Length; i++)
        {
            if (weaponContainers[i].Weapon == null)
            {
                weaponContainers[i].Weapon = weapon;
                _inventory.Remove(weapon);
                return true;
            }
        }
        return false;
    }
    public bool TryDeactivateWeapon(IWeapon weapon)
    {
        for (int i = 0; i < weaponContainers.Length; i++)
        {
            if (weaponContainers[i].Weapon == weapon)
            {
                _inventory.Add(weapon);
                weaponContainers[i].Weapon = null;
                return true;
            }
        }
        return false;
    }
    private Image _healthBar;
    private readonly float _maxHealth = 3;
    private float _health = 3;
    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthBar.fillAmount = _health / _maxHealth;
        if (_health <= 0)
        {
            PlayerDestroyed?.Invoke(this);
        }
    }
    public void SetHealthBarRef(GameObject obj)
    {
        _healthBar = obj.GetComponent<Image>();
        _healthBar.fillAmount = _health / _maxHealth;
    }
    private TextMeshProUGUI _scoreCounter;
    private int _score = 0;
    public int Score => _score;
    public void AddScore(int score)
    {
        _score += score;
        _scoreCounter.text = "Score: " + _score;
    }
    
    public void SetScoreCounterRef(GameObject obj)
    {
        _scoreCounter = obj.GetComponent<TextMeshProUGUI>();
    }

    void OnLook(InputValue input)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(input.Get<Vector2>());

        float zRotation = Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation + 90);

    }
    void Awake()
    {
        _fireAction = playerInput.actions["Fire"];
    }

    void Update()
    {
        if (_fireAction.IsPressed())
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (Vector2.Distance(mousePosition, new Vector2(transform.position.x, transform.position.y)) > 1f)
        {
            foreach (WeaponContainer container in weaponContainers)
            {
                container.Shoot();
            }
        }
    }
    void OnPause()
    {
        GamePaused?.Invoke(this);
    }
    public void EnablePauseMenuControls()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }
    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }
}
