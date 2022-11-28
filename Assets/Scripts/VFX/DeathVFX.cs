using UnityEngine;

[RequireComponent(typeof(HealthEntity))]
public class DeathVFX : MonoBehaviour
{
	[SerializeField] GameObject VFXPrefab;
	[SerializeField] float destroyTime;

	[SerializeField] float DieSFXVolume;
	[SerializeField] AudioClip DieSFX;

	void Start()
	{
		GetComponent<HealthEntity>().OnDeath.AddListener(SpawnDeathParticles);
	}

	void SpawnDeathParticles(HealthEntity healthEntity)
	{
		GameObject vfx = Instantiate(VFXPrefab, transform.position, transform.rotation);
		SFXManager.PlaySound(DieSFX, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup, DieSFXVolume, Random.Range(0.95f, 1.05f));

		if (destroyTime > 0)
		{
			Destroy(vfx, destroyTime);
		}
	}
}
