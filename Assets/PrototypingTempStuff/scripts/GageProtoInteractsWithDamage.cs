using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class GageProtoInteractsWithDamage : MonoBehaviour
{
    public Rigidbody rb;
    public Renderer renderer;
    Vector3 velocityBeforePhysicsUpdate;
    public bool invincible;
    public bool player;
    public float maxHealth;
    public float health;
    [SerializeField] TextMeshProUGUI healthText;

    public UnityEvent diedEvent;
    // Start is called before the first frame update
    void Start()
    {
        if(rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
        if(renderer == null)
        {
            renderer = gameObject.GetComponent<Renderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(healthText != null){
            healthText.text = health.ToString("F0") + "/" + maxHealth;
        }
        if(health <= 0 && invincible == false)
        {
            diedEvent.Invoke();
            if(player == false)
            {
                Destroy(this.gameObject);
            }
            else
            {
                renderer.material.color = Color.red;
            }
        }
    }
    void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = rb.velocity;
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.GetComponent<GageProtoInteractsWithDamage>() != null)
        {
            float collisionDamage = 0.5f * (other.gameObject.GetComponent<GageProtoInteractsWithDamage>().velocityBeforePhysicsUpdate.magnitude * other.gameObject.GetComponent<Rigidbody>().mass) + (velocityBeforePhysicsUpdate.magnitude *rb.mass);
            health -= collisionDamage;
            print(collisionDamage);
        }
    }
}
