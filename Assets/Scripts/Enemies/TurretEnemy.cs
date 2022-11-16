using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    //public GameObject turret;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = ((Player)FindObjectOfType(typeof(Player))).gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.position);
    }
}
