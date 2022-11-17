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
		/*foreach (var item in collision.contacts)
		{
			Debug.DrawRay(item.point, item.normal * -100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
		}*/
		ApplyImpact(collision.transform, collision.GetContact(0).normal * -1f);
	}

	private void ApplyImpact(Transform other, Vector3 contactNormal)
	{
		other.GetComponentInParent<IImpulseReceiver>()?.ApplyImpulse(
			contactNormal.normalized, 
			contactImpactorConfig.Knockback, 
			contactImpactorConfig.contactImpulseSourceType
		);
		other.GetComponentInParent<HealthEntity>()?.ApplyDamage(
			contactImpactorConfig.Damage, 
			contactImpactorConfig.contactDamageSourceType
		);
	}
}
