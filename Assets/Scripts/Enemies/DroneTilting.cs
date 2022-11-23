using UnityEngine;

public class DroneTilting : MonoBehaviour
{
	[SerializeField]
	private float maxTilt = 45.0f;
	[SerializeField]
	private float tiltFactor = 5.0f;
	[SerializeField]
	private float smoothingFactor = 5.0f;
	[SerializeField]
	Transform visuals;

	PhysicsData data;
	Transform facingTarget;

	private void Start()
	{
		data = GetComponent<PhysicsData>();
		facingTarget = FindObjectOfType<Player>().transform;
	}

	void Update()
	{
		Vector3 velocity = data.rb.velocity;
		// xTilt and zTilt are in degrees
		float xTilt = tiltFactor * velocity.x;
		float zTilt = tiltFactor * velocity.z;

		xTilt = Mathf.Clamp(xTilt, -maxTilt, maxTilt);
		zTilt = Mathf.Clamp(zTilt, -maxTilt, maxTilt);

		float yRot = visuals.rotation.eulerAngles.y;
		if (facingTarget)
		{
			yRot = Mathf.Atan2(facingTarget.position.z - transform.position.z, facingTarget.position.x - transform.position.x);
		}

		Quaternion newRotation = Quaternion.Euler(-xTilt, yRot, zTilt);
		visuals.rotation = Quaternion.Lerp(visuals.rotation, newRotation, Time.deltaTime * smoothingFactor);
	}
}
