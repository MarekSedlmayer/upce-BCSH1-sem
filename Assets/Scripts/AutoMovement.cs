using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AutoMovement : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float movementSpeed = 250f;
    private GameObject player;
    private Vector2 _targetPosition;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementVector;
    private LineRenderer _lineRenderer;

    private bool _ignorePlayer;
    private bool _destroyed = false;
    public bool Debug = false;
    public bool IsShooter = false;

    void Awake()
    {
        _targetPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
    }
    void OnEnable()
    {
        StartCoroutine(ActivateAfterSeconds(0.5f));
    }

    IEnumerator ActivateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (player == null)
            player = FindObjectOfType<PlayerMovement>().gameObject;
        _ignorePlayer = false;
    }

    void FixedUpdate()
    {
        if (player != null && !_ignorePlayer)
        {
            if (!IsShooter)
            {
                _targetPosition = player.transform.position;
            }
            else
            {
                if (!isActiveAndEnabled) return;
                StartCoroutine(FindAnotherPath(Random.value + 0.5f));
            }
        }
        _movementVector = (_targetPosition - (Vector2)transform.position).normalized;
        if (Vector2.Distance(_targetPosition, transform.position) > 0.05f)
            _rigidbody2D.AddForce(_movementVector * movementSpeed);
        #region Render debug line
        if (Debug)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _targetPosition);
        }
        #endregion
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out var player))
        {
            if (!_destroyed)
            {
                _destroyed = true;
                player.gameObject.GetComponentInChildren<Player>().TakeDamage(damage);
                var des = GetComponent<Destroyable>();
                des.TakeDamage(des.Health);
            }
        }
        else
        {
            if (!_ignorePlayer)
            {
                if (!isActiveAndEnabled) return;
                StartCoroutine(FindAnotherPath(Random.value + 0.5f, collision.GetContact(0).normal));
            }
        }
    }

    IEnumerator FindAnotherPath(float seconds, Vector2 collisionNormal)
    {
        _ignorePlayer = true;
        //_targetPosition = Random.insideUnitCircle * 2 + (Vector2)transform.position;
        float distortionFactor = 3;
        float signedRandomValue = (Random.value - 0.5f) * 2;
        Vector2 randomVector = (Vector2.Perpendicular(collisionNormal) * signedRandomValue) * distortionFactor;
        _targetPosition = (collisionNormal + randomVector) + (Vector2)transform.position;

        float elapsed = 0f;
        while (elapsed < seconds)
        {
            if (Vector2.Distance(_targetPosition, transform.position) < 0.05f) break;
            elapsed += Time.deltaTime;
            yield return null;
        }
        _ignorePlayer = false;
    }

    IEnumerator FindAnotherPath(float seconds)
    {
        _ignorePlayer = true;
        _targetPosition = Random.insideUnitCircle * 2 + (Vector2)transform.position;
        
        float elapsed = 0f;
        while (elapsed < seconds)
        {
            if (Vector2.Distance(_targetPosition, transform.position) < 0.05f) break;
            elapsed += Time.deltaTime;
            yield return null;
        }
        _ignorePlayer = false;
    }
}
