using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
	[SerializeField]
	float bulletSpeed;

	private void FixedUpdate()
	{
		transform.position += transform.forward * Time.deltaTime * bulletSpeed;
	}


	private void OnCollisionEnter(Collision collision)
	{
			Destroy(gameObject);
	}
}
