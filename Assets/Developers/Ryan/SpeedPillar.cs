using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPillar : MonoBehaviour
{

	[SerializeField]
	float speedBoostVelocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "SpeedPillar")
		{	
			GetComponent<Rigidbody>().velocity = speedBoostVelocity * (GetComponent<Rigidbody>().velocity.normalized);

		}

	}
}
