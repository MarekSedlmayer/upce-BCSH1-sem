using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private WeaponContainer[] weaponContainer = new WeaponContainer[2];
    [SerializeField] private WeaponScriptableObject wSO;
    private readonly WeaponFactory _weaponFactory = new WeaponFactory();

    private Vector2 _targetPosition;
    private GameObject _player;

    void Awake()
    {
        _targetPosition = transform.position;
    }
    void OnEnable()
    {
        StartCoroutine(ActivateAfterSeconds(0.5f));
    }

    IEnumerator ActivateAfterSeconds(float seconds)
    {
        for (int i = 0; i < weaponContainer.Length; i++)
        {
            weaponContainer[i].Weapon = _weaponFactory.Create(FindObjectOfType<PoolManager>().GetPool(wSO.ID), wSO);
        }
        yield return new WaitForSeconds(seconds);
        if (_player == null)
            _player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    void FixedUpdate()
    {
        if (_player != null)
        {
            _targetPosition = _player.transform.position;
            float zRotation = Mathf.Atan2(transform.position.y - _targetPosition.y, transform.position.x - _targetPosition.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(zRotation + 90f, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 5)
            {
                for (int i = 0; i < weaponContainer.Length; i++)
                {
                    weaponContainer[i].Shoot();
                }
            }
        }
    }
}
