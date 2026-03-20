using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
/// Requires 2 Collider2D components (trigger and non-trigger)
/// </remarks>
[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [SerializeField] private bool isLocked = false;

    private Collider2D _doorCollider;
    private Collider2D _proximityTrigger;
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    [HideInInspector]
    public Room ParentRoom;
    [HideInInspector]
    public Room AdjacentRoom;

    [SerializeField] private Teleporter teleporter;

    void Awake()
    {
        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            if (col.isTrigger) _proximityTrigger = col;
            else _doorCollider = col;
        }
        teleporter.PlayerTeleported += OnPlayerTeleported;
    }

    private void OnPlayerTeleported(Teleporter t)
    {
        ParentRoom.PlayerLeft();
        AdjacentRoom.PlayerEntered();
    }

    public void Open()
    {
        if (!isLocked && !_isOpen)
        {
            _doorCollider.enabled = false;
            _isOpen = true;
        }
    }

    public void Close()
    {
        if (_isOpen)
        {
            _doorCollider.enabled = true;
            _isOpen = false;
        }
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
