using UnityEngine;

// Triggers a Spawner at a regular time interval

[RequireComponent(typeof(Spawner))]
public class IntervalSpawner : MonoBehaviour
{
	public float SpawnInterval;

	protected Spawner spawner;
	protected float timer;

	private void Awake()
	{
		spawner = GetComponent<Spawner>();
	}

	void Update()
	{
		if (timer <= Mathf.Epsilon)
		{
			TrySpawn();
			timer = SpawnInterval;
		}

		timer -= Time.deltaTime;
	}

	protected virtual void TrySpawn()
	{
		spawner.Spawn();
	}
}
