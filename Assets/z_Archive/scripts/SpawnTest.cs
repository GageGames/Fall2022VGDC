using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
	public float spawnInterval;
	public Spawner spawner;

	float timer;

    void Update()
    {
        if (timer <= Mathf.Epsilon)
		{
			spawner.Spawn();

			timer = spawnInterval;
		}

		timer -= Time.deltaTime;

	}
}
