using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lilForce : MonoBehaviour
{
    [SerializeField] float forceAmount;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * forceAmount, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
