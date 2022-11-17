using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
	[SerializeField]
	[Expandable]
	PitConfig pitConfig;
	/*
	[SerializeField]
	float tickLength;

	float tickTime = 0.0f;*/

	HashSet<HealthEntity> stuffInPit = new HashSet<HealthEntity>();

	void Update()
	{
		foreach (HealthEntity he in stuffInPit)
		{
			he.ApplyDamage(pitConfig.DamagePerSecond * Time.deltaTime);
		}
		/*
		tickTime += Time.fixedDeltaTime;
		if (tickTime > tickLength)
		{
			foreach (var obj in stuffInPit)
			{
				if (obj != null)
				{
					obj.GetComponent<GageProtoInteractsWithDamage>().thisRenderer.material.DOColor(Color.black, tickTime / 2.1f).SetLoops(2, LoopType.Yoyo);
				}
			}

			tickTime = 0.0f;
		}*/
	}

	void OnTriggerEnter(Collider other)
	{
		// TODO: More stable way of determining what should be affected by the pit
		if (other.GetComponentInParent<HealthEntity>())
		{
			stuffInPit.Add(other.GetComponentInParent<HealthEntity>());
		}
	}

	void OnTriggerExit(Collider other)
	{
		// TODO: More stable way of determining what should be affected by the pit
		if (other.GetComponentInParent<HealthEntity>())
		{
			stuffInPit.Add(other.GetComponentInParent<HealthEntity>());
		}
	}
}
