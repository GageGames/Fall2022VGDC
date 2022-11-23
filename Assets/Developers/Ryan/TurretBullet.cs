using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
	[SerializeField]
	float bulletSpeed;
	Rigidbody rg;

    // Start is called before the first frame update
    void Start()
    {
		rg = GetComponent<Rigidbody>();
    }

	private void FixedUpdate()
	{
		transform.position += transform.forward * Time.deltaTime * bulletSpeed;
	}

	// Update is called once per frame
	void Update()
    {
		//rg.AddForce(new Vector3( 0f, 0f, bulletSpeed));
		//transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

	private void OnCollisionEnter(Collision collision)
	{
			Destroy(gameObject);
	}
}
