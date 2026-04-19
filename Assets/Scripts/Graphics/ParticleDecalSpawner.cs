using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalSpawner : MonoBehaviour
{
    [SerializeField] private TextureGroupScriptableObject textureGroup;
    [SerializeField] private bool randomRotation = true;

    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particles = new ParticleSystem.Particle[(int)_particleSystem.emission.GetBurst(0).count.constant];
    }

    private void LateUpdate()
    {
        if (_particleSystem.isPlaying)
        {
            if ((_particleSystem.main.duration - _particleSystem.time) < 1f)
            {
                int numOfParticles = _particleSystem.GetParticles(_particles);
                for (int i = 0; i < numOfParticles; i++)
                {
                    Spawn(transform.TransformPoint(_particles[i].position));
                }
                _particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }

    public void Spawn(Vector2 position)
    {
        Texture2D tex = textureGroup.Textures[Random.Range(0, textureGroup.Textures.Length)];
        FindObjectOfType<GlobalDecalManager>().RequestDecal(tex, position, tex.height, randomRotation ? Random.Range(0, 360) : 0);
    }
}
