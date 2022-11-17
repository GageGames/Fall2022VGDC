using System.Collections.Generic;
using UnityEngine;

public class CappedIntervalSpawner : IntervalSpawner
{
	[SerializeField] int EntityCap;

	List<HealthEntity> spawnedObjects = new List<HealthEntity>();

	protected override void TrySpawn()
	{
		if (spawnedObjects.Count >= EntityCap)
		{
			return;
		}

		foreach (GameObject obj in spawner.Spawn())
		{
			if (!obj.GetComponent<HealthEntity>())
			{
				Debug.LogError("Capped interval spawners should only be used to spawn entities that can die! Use HealthEntity plz ;-;");
				return;
			}

			spawnedObjects.Add(obj.GetComponent<HealthEntity>());
			obj.GetComponent<HealthEntity>().OnDeath.AddListener(OnEntityDeath);
		}
	}

	void OnEntityDeath(HealthEntity healthEntity)
	{
		spawnedObjects.Remove(healthEntity);
	}
}
