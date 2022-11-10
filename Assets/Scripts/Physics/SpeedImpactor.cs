using UnityEngine;

[RequireComponent(typeof(PhysicsData))]
public class SpeedImpactor : Impactor
{
	[HideInInspector]
	public float ImpactThreshold = 5f;

	PhysicsData physData;
	HealthEntity health;

	HealthEffectSourceType impactDamageSourceType = new HealthEffectSourceType(HealthEffectSourceTag.Impact);

	protected override void Awake()
	{
		if (!impactorConfig)
		{
			Debug.LogError("No ImpactorConfig assigned!");
			return;
		}

		ConstantContactDamage = impactorConfig.ConstantContactDamage;
		ConstantContactKnockback = impactorConfig.ConstantContactKnockback;

		physData = GetComponent<PhysicsData>();
		health = GetComponent<HealthEntity>();
	}

	protected override void OnValidate()
	{
		base.OnValidate();

		if (impactorConfig == null) return;

		if (impactorConfig.GetType() != typeof(SpeedImpactorConfig))
		{
			Debug.LogError("ImpactorConfig assigned must be of type SpeedImpactorConfig");
			impactorConfig = null;
		}
	}

	protected override void ApplyImpact(Transform other)
	{
		base.ApplyImpact(other);

		float otherMass = 1f;
		Vector3 velocityDifferential = physData.rb.velocity;

		if (other.GetComponent<PhysicsData>())
		{
			Rigidbody otherRb = other.GetComponent<PhysicsData>().rb;
			otherMass = otherRb.mass;
			velocityDifferential -= otherRb.velocity;
		}
		else if (other.GetComponent<Rigidbody>())
		{
			Debug.LogWarning("All Rigidbodies that can be impacted should have PhysicsData somewhere on them!");
			Rigidbody otherRb = other.GetComponent<Rigidbody>();
			otherMass = otherRb.mass;
			velocityDifferential -= otherRb.velocity;
		}

		if (velocityDifferential.magnitude * (physData.rb.mass + otherMass) < ImpactThreshold)
		{
			Debug.Log("Impact damage below threshold, not applied");
			return;
		}

		other.GetComponent<HealthEntity>()?.ApplyDamage(physData.rb.mass * velocityDifferential.magnitude, impactDamageSourceType);
		health?.ApplyDamage(otherMass * velocityDifferential.magnitude, impactDamageSourceType);

		Debug.Log($"Applied {otherMass * velocityDifferential.magnitude} damage to self and {physData.rb.mass * velocityDifferential.magnitude} to other");
	}
}
