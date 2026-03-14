using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private float _timeToLive = 5f;
    private float _bulletSpeed = 20f;
    private float _damage = 1f;
    private Rigidbody2D _rigidbody2D;

    public void Init(float timeToLive, float speed, float damage)
    {
        _timeToLive = timeToLive;
        _bulletSpeed = speed;
        _damage = damage;
    }

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _rigidbody2D.AddForce(transform.up * _bulletSpeed, ForceMode2D.Impulse);
        Invoke(nameof(Disable), _timeToLive);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Destroyable>(out var destroyable))
        {
            destroyable.TakeDamage(_damage);
            CancelInvoke(nameof(Disable));
            Disable();
        }
    }
}
