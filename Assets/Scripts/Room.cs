using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Connected rooms")]
    [SerializeField] private Room roomTop;
    [SerializeField] private Room roomBottom;
    [SerializeField] private Room roomLeft;
    [SerializeField] private Room roomRight;
    [Header("Doors")]
    [SerializeField] private GameObject doorTop;
    [SerializeField] private GameObject doorBottom;
    [SerializeField] private GameObject doorLeft;
    [SerializeField] private GameObject doorRight;

    private bool isEmpty = true;
    //private bool isPlayerIn = false;

    void Start()
    {
        if (/*roomTop != null && */isEmpty)
        {
            doorTop.GetComponent<Collider2D>().enabled = false;
        }
    }
}
