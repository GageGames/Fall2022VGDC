using UnityEngine;

//[RequireComponent(typeof(Health))]
public class ExplosiveEntity : MonoBehaviour
{
	[SerializeField] float ExplosionRadius = 5f;
	[SerializeField] float ExplosionStrength = 5f;
	[SerializeField] float ExplosionDamage = 5f;

	//Health health;

	private void Awake()
	{
		//health = GetComponent<Health>();

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
				//col.GetComponent<Health>()?.TakeDamage(ExplosionDamage);
			}
		}
	}
}
