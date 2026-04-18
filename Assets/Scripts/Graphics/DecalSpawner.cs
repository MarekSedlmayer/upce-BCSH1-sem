using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalSpawner : MonoBehaviour
{
    [SerializeField] private Texture2D craterTexture;
    [SerializeField] private bool randomRotation = true;

    public void Spawn()
    {
        FindObjectOfType<GlobalDecalManager>().RequestDecal(craterTexture, transform.position, 64f, randomRotation ? Random.Range(0, 360) : 0);
    }
}
