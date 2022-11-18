using UnityEngine;

// Object that applies damage and/or knockback to other objects on contact

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
		// if the other object is not on a layer contained in the layermask, stop
		if ((contactImpactorConfig.TargetLayers & (1 << other.gameObject.layer)) == 0) {
			return;
		}

		other.GetComponentInParent<IImpulseReceiver>()?.ApplyImpulse(
			contactNormal.normalized, 
			contactImpactorConfig.Knockback, 
			contactImpactorConfig.contactImpulseSourceType
		);
		//Debug.Log($"Damaging {other.name} by {contactImpactorConfig.Damage}");
		/*if (other.GetComponentInParent<HealthEntity>())
		{
			Debug.Log($"Damaging {other.GetComponentInParent<HealthEntity>().name} by {contactImpactorConfig.Damage}");
		}*/
		other.GetComponentInParent<HealthEntity>()?.ApplyDamage(
			contactImpactorConfig.Damage, 
			contactImpactorConfig.contactDamageSourceType
		);
	}
}
