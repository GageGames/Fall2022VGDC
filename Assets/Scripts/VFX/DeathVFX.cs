using UnityEngine;

[RequireComponent(typeof(HealthEntity))]
public class DeathVFX : MonoBehaviour
{
	[SerializeField] GameObject VFXPrefab;
	[SerializeField] float destroyTime;

	void Start()
	{
		GetComponent<HealthEntity>().OnDeath.AddListener(SpawnDeathParticles);
	}

	void SpawnDeathParticles(HealthEntity healthEntity)
	{
		GameObject vfx = Instantiate(VFXPrefab, transform.position, transform.rotation);
		if (destroyTime > 0)
		{
			Destroy(vfx, destroyTime);
		}
	}
}
