using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextureGroupSO_", menuName = "ScriptableObjects/TextureGroupSO", order = 3)]
public class TextureGroupScriptableObject : ScriptableObject
{
    [SerializeField] private Texture2D[] textures;
    public Texture2D[] Textures => textures;
}
