using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeToLive = 10f;
    [SerializeField] private float bulletSpeed = 100f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);

        Destroy(gameObject, timeToLive);
    }

    void Update()
    {

    }
}
