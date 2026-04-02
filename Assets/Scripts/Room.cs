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

    private bool _isEmpty; // All visible enemies are gone.
    private bool _isPlayerIn = false;
    public bool IsEmpty => _isEmpty;
    public bool IsPlayerIn => _isPlayerIn;


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
    }
    void Start()
    {
        OpenDoors();
    }

    void UpdateRoom()
    {
        _isEmpty = _visibleEnemies.Count == 0;

        if (_isEmpty)
        {
            OpenDoors();
        }
        else
        {
            CloseDoors();
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
    public void PlayerEntered()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        _isPlayerIn = true;
        UpdateRoom();
    }
}
