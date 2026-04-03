using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Destroyable : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool destroyObject = false;
    public event Action<Destroyable> Destroyed;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroyed?.Invoke(this);
            if (destroyObject) Destroy(gameObject);
        }
    }
}
