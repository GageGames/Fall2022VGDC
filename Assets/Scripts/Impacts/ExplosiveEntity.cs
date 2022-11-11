using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthEntity))]
public class ExplosiveEntity : MonoBehaviour
{
	[HideInInspector]
	public UnityEvent OnExplode = new UnityEvent();

	[SerializeField] float ExplosionRadius = 5f;
	[SerializeField] float ExplosionStrength = 5f;
	[SerializeField] float ExplosionDamage = 5f;

	HealthEntity health;
	HealthEffectSourceType explosiveDamageSourceType = new HealthEffectSourceType(HealthEffectSourceTag.Explosive);

	private void Awake()
	{
		health = GetComponent<HealthEntity>();

		health.OnDeath.AddListener (Explode);
	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

		if (colliders.Length > 0)
		{
			foreach (Collider col in colliders)
			{
				if (col.gameObject != gameObject)
				{
					Vector3 dir = col.transform.position - transform.position;
					dir.y = 0;
					dir = dir.normalized;

					col.GetComponent<IImpulseReceiver>()?.ApplyImpulse(dir, ExplosionStrength);
					col.GetComponent<HealthEntity>()?.ApplyDamage(ExplosionDamage, explosiveDamageSourceType);
				}
			}
		}

		OnExplode?.Invoke();
	}
}
