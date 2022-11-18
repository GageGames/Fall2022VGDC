using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HealthEntity))]
public class ExplosiveEntity : MonoBehaviour
{
	[HideInInspector]
	public UnityEvent OnExplode = new UnityEvent();

	[Expandable]
	[SerializeField] ExplosiveConfig explosiveConfig;

	HealthEntity health;
	HealthEffectSourceType explosiveDamageSourceType = new HealthEffectSourceType(HealthEffectSourceTag.Explosive);

	private void Awake()
	{
		health = GetComponent<HealthEntity>();

		health.OnDeath.AddListener (Explode);
	}

	void Explode(HealthEntity entity)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveConfig.ExplosionRadius);
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
				IImpulseReceiver targetImpulseReceiver = col.gameObject.GetComponentInParent<IImpulseReceiver>();
				HealthEntity targetHealthEntity = col.gameObject.GetComponentInParent<HealthEntity>();

				if (targetHealthEntity != health)
				{
					Vector3 dir = col.transform.position - transform.position;
					dir.y = 0;

					float falloffFactor = explosiveConfig.ExplosionFalloff.Evaluate(dir.magnitude / explosiveConfig.ExplosionRadius);

					targetImpulseReceiver?.ApplyImpulse(dir.normalized, explosiveConfig.ExplosionStrength * falloffFactor);
					targetHealthEntity?.ApplyDamage(explosiveConfig.ExplosionDamage * falloffFactor, explosiveDamageSourceType);
				}
			}
		}

		OnExplode?.Invoke();
	}
}
