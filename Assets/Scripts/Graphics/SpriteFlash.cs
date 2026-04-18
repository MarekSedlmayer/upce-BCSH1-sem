using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    [SerializeField] private float duration = 0.1f;

    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    private float _timer;
    private Material[] _materials;

    void Awake()
    {
        _materials = new Material[_spriteRenderers.Length];
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i] = _spriteRenderers[i].material;
        }
    }

    public void Flash()
    {
        _timer = duration;
        enabled = true;
    }
    void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;

            float t = _timer / duration;
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat("_FlashAmount", t);
            }
        }
        else
        {
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetFloat("_FlashAmount", 0f);
            }
            enabled = false;
        }
    }
}
