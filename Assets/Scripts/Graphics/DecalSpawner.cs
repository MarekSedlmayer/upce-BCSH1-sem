using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalSpawner : MonoBehaviour
{
    [SerializeField] private TextureGroupScriptableObject textureGroup;
    [SerializeField] private bool randomRotation = true;

    public void Spawn()
    {
        Texture2D tex = textureGroup.Textures[Random.Range(0, textureGroup.Textures.Length)];
        FindObjectOfType<GlobalDecalManager>().RequestDecal(tex, transform.position, tex.height, randomRotation ? Random.Range(0, 360) : 0);
    }
}
