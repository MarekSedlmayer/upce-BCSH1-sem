using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    void OnLook(InputValue input)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(input.Get<Vector2>());

        float zRotation = Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);

    }

    void OnFire()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (Vector2.Distance(mousePosition, new Vector2(transform.position.x, transform.position.y)) > 1f)
        {
            //Activate bullet
        }
    }
}
