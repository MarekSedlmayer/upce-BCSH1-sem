using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Destroyable : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool destroyObject = false;
    public event Action<Destroyable> Destroyed;
    [SerializeField] private UnityEvent DestroyedEvent;
    public float Health => health;

    private bool _destroyed = false;

    public void TakeDamage(float damage)
    {
        if (!_destroyed)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroyed?.Invoke(this);
                DestroyedEvent.Invoke();
                if (destroyObject) Destroy(gameObject);
                _destroyed = true;
            }
        }
    }
}
