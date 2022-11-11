using UnityEngine;

// Object that deals damage and/or knockback to object objects on contact

public class ContactImpactor : MonoBehaviour
{
	[Expandable]
	public ContactImpactorConfig contactImpactorConfig;

	private void Awake()
	{
		if (!contactImpactorConfig)
		{
			Debug.LogError("No ImpactorConfig assigned!");
			return;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		ApplyImpact(collision.transform.root, collision.GetContact(0).normal);
	}

	private void ApplyImpact(Transform other, Vector3 contactNormal)
	{
		other.GetComponent<IImpulseReceiver>()?.ApplyImpulse(
			contactNormal.normalized, 
			contactImpactorConfig.Knockback, 
			contactImpactorConfig.contactImpulseSourceType
		);
		other.GetComponent<HealthEntity>()?.ApplyDamage(
			contactImpactorConfig.Damage, 
			contactImpactorConfig.contactDamageSourceType
		);
	}
}
