using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPillar : MonoBehaviour
{

	[SerializeField]
	float speedBoostVelocity;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "SpeedPillar")
		{	
			GetComponent<Rigidbody>().velocity = speedBoostVelocity * (GetComponent<Rigidbody>().velocity.normalized);

		}
	}
}
