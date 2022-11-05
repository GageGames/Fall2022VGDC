using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageable : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocityBeforePhysicsUpdate;
    public float maxHealth;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = rb.velocity;
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.GetComponent<damageable>() != null)
        {
            float collisionDamage = 0.5f * (other.gameObject.GetComponent<damageable>().velocityBeforePhysicsUpdate.magnitude * other.gameObject.GetComponent<Rigidbody>().mass) + (velocityBeforePhysicsUpdate.magnitude *rb.mass);
            health -= collisionDamage;
            print(collisionDamage);
        }
    }
}
