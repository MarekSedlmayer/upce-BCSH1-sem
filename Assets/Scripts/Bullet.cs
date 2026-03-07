using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeToLive = 10f;
    [SerializeField] private float bulletSpeed = 100f;

    private Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _rigidbody2D.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Invoke(nameof(Disable), timeToLive);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }
}
