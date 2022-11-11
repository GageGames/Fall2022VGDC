using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] float interval;
    float timer = 0f;

    void Update() 
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = interval;
        }
    }
    void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
