using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impactor : MonoBehaviour
{

	public float ContactDamage = 0f;
	//DamageSourceType contactDamageSourceType = new DamageSourceType(DamageSourceTag.Contact);

	public float ContactKnockback = 0f;
	ImpulseSourceType contactImpulseSourceType = new ImpulseSourceType(ImpulseSourceTag.PhysicalKnockback);

	private void OnValidate()
	{
		contactImpulseSourceType.impulseSourceTag = ContactDamage <= Mathf.Epsilon ? ImpulseSourceTag.PhysicalKnockback : ImpulseSourceTag.DangerousKnockback;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Transform colTrans = collision.transform;
		while (colTrans.parent != null)
		{
			colTrans = colTrans.parent;
		}

		ApplyImpact(colTrans);
	}

	protected virtual void ApplyImpact (Transform other)
	{
		other.GetComponent<IImpulseReceiver>()?.ApplyImpulse((other.transform.position - transform.position).normalized, ContactKnockback, contactImpulseSourceType);
		//colTrans.GetComponent<Health>()?.Damage(ContactDamage, contactDamageSourceType);
	}
}
