using UnityEngine;

public class Impactor : MonoBehaviour
{
	[HideInInspector]
	public float ConstantContactDamage = 0f;
	[HideInInspector]
	public float ConstantContactKnockback = 0f;

	public ImpactorConfig impactorConfig;

	HealthEffectSourceType contactDamageSourceType = new HealthEffectSourceType(HealthEffectSourceTag.Impact);
	ImpulseSourceType contactImpulseSourceType = new ImpulseSourceType(ImpulseSourceTag.PhysicalKnockback);

	protected virtual void OnValidate()
	{
		contactImpulseSourceType.impulseSourceTag = ConstantContactDamage <= Mathf.Epsilon ? ImpulseSourceTag.PhysicalKnockback : ImpulseSourceTag.DangerousKnockback;
	}

	protected virtual void Awake()
	{
		if (!impactorConfig)
		{
			Debug.LogError("No ImpactorConfig assigned!");
			return;
		}

		ConstantContactDamage = impactorConfig.ConstantContactDamage;
		ConstantContactKnockback = impactorConfig.ConstantContactKnockback;
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

	protected virtual void ApplyImpact(Transform other)
	{
		other.GetComponent<IImpulseReceiver>()?.ApplyImpulse((other.transform.position - transform.position).normalized, ConstantContactKnockback, contactImpulseSourceType);
		other.GetComponent<HealthEntity>()?.ApplyDamage(ConstantContactDamage, contactDamageSourceType);
	}
}
