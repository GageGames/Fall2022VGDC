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
	[SerializeField] AnimationCurve ExplosionFalloff;

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
		/*
		GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		debugSphere.transform.position = transform.position;
		debugSphere.transform.localScale = Vector3.one * ExplosionRadius;
		debugSphere.GetComponent<Collider>().enabled = false;
		Destroy(debugSphere, 5f);*/

		if (colliders.Length > 0)
		{
			foreach (Collider col in colliders)
			{
				Transform target = col.transform.root;

				if (target.gameObject != gameObject)
				{
					Vector3 dir = target.position - transform.position;
					dir.y = 0;

					float falloffFactor = ExplosionFalloff.Evaluate(dir.magnitude / ExplosionRadius);

					target.GetComponent<IImpulseReceiver>()?.ApplyImpulse(dir.normalized, ExplosionStrength * falloffFactor);
					target.GetComponent<HealthEntity>()?.ApplyDamage(ExplosionDamage * falloffFactor, explosiveDamageSourceType);
				}
			}
		}

		OnExplode?.Invoke();
	}
}
