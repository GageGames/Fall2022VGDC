using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField]
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);

    }
}
