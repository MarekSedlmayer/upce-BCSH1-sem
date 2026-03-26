using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ProfileData ProfileData;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
