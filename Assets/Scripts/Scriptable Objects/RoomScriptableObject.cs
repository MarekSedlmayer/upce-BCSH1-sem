using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomSO_", menuName = "ScriptableObjects/RoomSO", order = 2)]
public class RoomScriptableObject : ScriptableObject
{
    [SerializeField] private string id;
    public string ID => id;
}
