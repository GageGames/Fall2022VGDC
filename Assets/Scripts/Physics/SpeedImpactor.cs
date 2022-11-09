using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsData))]
public class SpeedImpactor : Impactor
{
	PhysicsData physData;
	//Health health;

	//DamageSourceType impactDamageSourceType = new DamageSourceType(DamageSourceTag.Impact);

	private void Awake()
	{
		physData = GetComponent<PhysicsData>();
		//health = GetComponent<Health>();
	}

	protected override void ApplyImpact (Transform other)
	{
		base.ApplyImpact (other);

		float otherMass = 1f;
		float otherSpeed = 0f;

		if (other.GetComponent<PhysicsData>())
		{
			Rigidbody otherRb = other.GetComponent<PhysicsData>().rb;
			otherMass = otherRb.mass;
			otherSpeed = otherRb.velocity.magnitude;
		} else if (other.GetComponent<Rigidbody>())
		{
			Debug.LogWarning("All Rigidbodies that can be impacted should have PhysicsData somewhere on them!");
			Rigidbody otherRb = other.GetComponent<Rigidbody>();
			otherMass = otherRb.mass;
			otherSpeed = otherRb.velocity.magnitude;
		}

		float dmg = (physData.rb.mass * physData.rb.velocity.magnitude) + (otherMass * otherSpeed);

		//colTrans.GetComponent<Health>()?.Damage(dmg, impactDamageSourceType);
		//health?.Damage(dmg, impactDamageSourceType);
	}
}
