using UnityEngine;

// Object that applies velocity-based impact damage to other objects on contact

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
		ApplyImpact(collision.gameObject);
	}

	private void ApplyImpact(GameObject other)
	{
		Vector3 velocityDifferential = physData.rb.velocity;
		float otherMass = 0;

		PhysicsData otherData = other.GetComponentInParent<PhysicsData>();

		if (otherData)
		{
			velocityDifferential -= otherData.rb.velocity;
			otherMass = otherData.rb.mass;
		}
		else if (other.GetComponentInParent<Rigidbody>())
		{
			Debug.LogWarning("All Rigidbodies that can be impacted should have PhysicsData somewhere on them!");
			velocityDifferential -= other.GetComponentInParent<Rigidbody>().velocity;
			otherMass = other.GetComponentInParent<Rigidbody>().mass;
		}

		other.GetComponentInParent<HealthEntity>()?.ApplyDamage((physData.rb.mass + otherMass) * velocityDifferential.magnitude, impactDamageSourceType);

		//Debug.Log($"Applied {physData.rb.mass * velocityDifferential.magnitude} damage to other");
	}
}
