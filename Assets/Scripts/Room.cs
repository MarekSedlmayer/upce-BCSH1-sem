using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> _visibleEnemies = new List<GameObject>();
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private RoomScriptableObject roomSO;
    public string GetID() => roomSO.ID;

    [Header("Connected rooms")]
    [SerializeField] private Room roomTop;
    [SerializeField] private Room roomBottom;
    [SerializeField] private Room roomLeft;
    [SerializeField] private Room roomRight;
    [Header("Doors")]
    [SerializeField] private Door doorTop;
    [SerializeField] private Door doorBottom;
    [SerializeField] private Door doorLeft;
    [SerializeField] private Door doorRight;

    private bool _isCleared; // All visible enemies are gone.
    private bool _isPlayerIn = false;

    public bool IsCleared => _isCleared;
    public bool IsPlayerIn => _isPlayerIn;

    private Player _playerScript = null;

    public void SetCleared(bool isCleared)
    {
        _isCleared = isCleared;
    }

    void Awake()
    {
        doorTop.ParentRoom = this;
        doorTop.AdjacentRoom = roomTop;
        doorBottom.ParentRoom = this;
        doorBottom.AdjacentRoom = roomBottom;
        doorLeft.ParentRoom = this;
        doorLeft.AdjacentRoom = roomLeft;
        doorRight.ParentRoom = this;
        doorRight.AdjacentRoom = roomRight;

        if (roomTop == null) doorTop.Hide();
        if (roomBottom == null) doorBottom.Hide();
        if (roomLeft == null) doorLeft.Hide();
        if (roomRight == null) doorRight.Hide();

        if (_visibleEnemies.Count == 0)
            _isCleared = true;
    }
    void Start()
    {
        OpenDoors();
    }

    private void OnEnemyDestroyed(Destroyable destroyable)
    {
        destroyable.gameObject.SetActive(false);
        _visibleEnemies.Remove(destroyable.gameObject);

        _playerScript.AddScore(5);

        if (_visibleEnemies.Count == 0)
        {
            _isCleared = true;
            _playerScript.IsInEmptyRoom = IsCleared;
            OpenDoors();
        }
    }

    void OpenDoors()
    {
        if (roomTop != null) doorTop.Open();
        if (roomBottom != null) doorBottom.Open();
        if (roomLeft != null) doorLeft.Open();
        if (roomRight != null) doorRight.Open();
    }

    void CloseDoors()
    {
        if (roomTop != null) doorTop.Close();
        if (roomBottom != null) doorBottom.Close();
        if (roomLeft != null) doorLeft.Close();
        if (roomRight != null) doorRight.Close();
    }

    public void PlayerLeft()
    {
        _isPlayerIn = false;
    }
    public void PlayerEntered(GameObject playerObject)
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        _isPlayerIn = true;

        if (_playerScript == null)
        {
            _playerScript = playerObject.GetComponentInChildren<Player>();
        }
        if (_visibleEnemies.Count == 0)
            _isCleared = true;
        _playerScript.IsInEmptyRoom = IsCleared;
        if (!_isCleared)
        {
            CloseDoors();
            foreach (var enemy in _visibleEnemies)
            {
                enemy.SetActive(true);
                enemy.GetComponent<Destroyable>().Destroyed += OnEnemyDestroyed;
            }
        }
        else
        {
            OpenDoors();
        }
    }
}
