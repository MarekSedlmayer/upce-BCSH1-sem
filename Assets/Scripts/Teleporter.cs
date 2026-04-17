using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    public event Action<Teleporter, PlayerMovement> PlayerTeleported;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out var player))
        {
            player.transform.position = new Vector3(destination.position.x, destination.position.y, 0);
            PlayerTeleported?.Invoke(this, player);
        }
    }
}
