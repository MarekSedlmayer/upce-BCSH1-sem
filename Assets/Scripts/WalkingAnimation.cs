using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Animator animator;

    private IMovementProvider _movement;

    void Awake()
    {
        _movement = GetComponentInParent<IMovementProvider>();
    }

    void Update()
    {
        Vector2 move = _movement.GetNormalizedMovementVector();

        bool isMoving = move.sqrMagnitude > 0.001f;
        float speed = move.magnitude;
        if (animator != null)
        {
            animator.speed = isMoving ? 1f : 0f;
        }

        if (!isMoving)
            return;

        float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;

        Quaternion targetRot = Quaternion.Euler(0, 0, angle - 90f);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }
}
