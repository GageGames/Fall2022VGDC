using MyBox;
using System.Collections.Generic;
using UnityEngine;

// Deals continuous damage to all objects in contact

public class Pit : MonoBehaviour
{
	[Expandable]
	[SerializeField] PitConfig pitConfig;

	[SerializeField] float DamageSFXVolume;
	[SerializeField] AudioClip DamageSFX;
	[SerializeField] float TriggerTime = 1f;

	HashSet<HealthEntity> stuffInPit = new HashSet<HealthEntity>();

	HealthEffectSourceType pitDamageSourceType = new HealthEffectSourceType(HealthEffectSourceTag.Pit);

	List<float> timers = new List<float>();

	void Update()
	{
		int itr = 0;
		foreach (HealthEntity he in stuffInPit)
		{
			he.ApplyDamage(pitConfig.DamagePerSecond * Time.deltaTime, pitDamageSourceType);

			timers[itr] += Time.deltaTime;
			if (timers[itr] >= TriggerTime)
			{
				SFXManager.PlaySound(DamageSFX, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup, DamageSFXVolume, Random.Range(0.95f, 1.05f));
				timers[itr] = 0;
			}
			itr++;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// TODO: More reliable way of finding HealthEntity
		if (other.GetComponentInParent<HealthEntity>())
		{
			stuffInPit.Add(other.GetComponentInParent<HealthEntity>());
			timers.Add(0);
		}
	}

	void OnTriggerExit(Collider other)
	{
		// TODO: More reliable way of finding HealthEntity
		if (other.GetComponentInParent<HealthEntity>())
		{
			int index = stuffInPit.IndexOfItem(other.GetComponentInParent<HealthEntity>());
			stuffInPit.Remove(other.GetComponentInParent<HealthEntity>());
			timers.RemoveAt(index);
		}
	}
}
