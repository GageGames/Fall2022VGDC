using UnityEngine;

[RequireComponent(typeof(PhysicsData))]
public class Impactable : MonoBehaviour
{
	PhysicsData physData;

	HealthEffectSourceType impactDamageSourceType = new HealthEffectSourceType(HealthEffectSourceTag.Impact);

	private void Awake()
	{
		physData = GetComponent<PhysicsData>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		ApplyImpact(collision.transform.root);
	}

	private void ApplyImpact(Transform other)
	{
		Vector3 velocityDifferential = physData.rb.velocity;
		float otherMass = 0;

		if (other.GetComponent<PhysicsData>())
		{
			velocityDifferential -= other.GetComponent<PhysicsData>().rb.velocity;
			otherMass = other.GetComponent<PhysicsData>().rb.mass;
		}
		else if (other.GetComponent<Rigidbody>())
		{
			Debug.LogWarning("All Rigidbodies that can be impacted should have PhysicsData somewhere on them!");
			velocityDifferential -= other.GetComponent<Rigidbody>().velocity;
			otherMass = other.GetComponent<Rigidbody>().mass;
		}

		other.GetComponent<HealthEntity>()?.ApplyDamage((physData.rb.mass + otherMass) * velocityDifferential.magnitude, impactDamageSourceType);

		//Debug.Log($"Applied {physData.rb.mass * velocityDifferential.magnitude} damage to other");
	}
}
