using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProfileData
{
    public string ProfileName;
    public Vector3 PlayerPosition;
    public string[] WeaponsInUse = new string[4];
    public bool FirstTime = true;
}
