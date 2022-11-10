using UnityEngine;

[RequireComponent(typeof(HealthEntity))]
public class ExplosiveEntity : MonoBehaviour
{
	[SerializeField] float ExplosionRadius = 5f;
	[SerializeField] float ExplosionStrength = 5f;
	[SerializeField] float ExplosionDamage = 5f;

	HealthEntity health;
	HealthEffectSourceType explosiveDamageSourceType = new HealthEffectSourceType(HealthEffectSourceTag.Explosive);

	private void Awake()
	{
		health = GetComponent<HealthEntity>();

		//health.OnDeath.AddListener (Explode);
	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

		if (colliders.Length > 0)
		{
			foreach (Collider col in colliders)
			{
				col.GetComponent<IImpulseReceiver>()?.ApplyImpulse((col.transform.position - transform.position).normalized, ExplosionStrength);
				col.GetComponent<HealthEntity>()?.ApplyDamage(ExplosionDamage, explosiveDamageSourceType);
			}
		}
	}
}
