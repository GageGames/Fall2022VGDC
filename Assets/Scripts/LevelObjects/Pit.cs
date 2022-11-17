using System.Collections.Generic;
using UnityEngine;

// Deals continuous damage to all objects in contact

public class Pit : MonoBehaviour
{
	[Expandable]
	[SerializeField] PitConfig pitConfig;

	HashSet<HealthEntity> stuffInPit = new HashSet<HealthEntity>();

	HealthEffectSourceType pitDamageSourceType = new HealthEffectSourceType(HealthEffectSourceTag.Pit);

	void Update()
	{
		foreach (HealthEntity he in stuffInPit)
		{
			he.ApplyDamage(pitConfig.DamagePerSecond * Time.deltaTime, pitDamageSourceType);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// TODO: More reliable way of finding HealthEntity
		if (other.GetComponentInParent<HealthEntity>())
		{
			stuffInPit.Add(other.GetComponentInParent<HealthEntity>());
		}
	}

	void OnTriggerExit(Collider other)
	{
		// TODO: More reliable way of finding HealthEntity
		if (other.GetComponentInParent<HealthEntity>())
		{
			stuffInPit.Remove(other.GetComponentInParent<HealthEntity>());
		}
	}
}
