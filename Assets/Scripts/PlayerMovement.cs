using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 350f;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementVector;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidbody2D.AddForce(_movementVector * movementSpeed);
    }

    void OnMove(InputValue movementValue)
    {
        _movementVector = movementValue.Get<Vector2>();
    }
}