using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    public event Action<Teleporter> PlayerTeleported;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Movement>(out var player))
        {
            player.transform.position = destination.position;
            PlayerTeleported.Invoke(this);
        }
    }
}
