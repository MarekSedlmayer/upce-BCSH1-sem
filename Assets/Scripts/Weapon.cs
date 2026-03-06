using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
