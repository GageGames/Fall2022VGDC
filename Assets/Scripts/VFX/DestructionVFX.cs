using UnityEngine;

public class DestructionVFX : MonoBehaviour
{
	[SerializeField] GameObject VFXPrefab;
	[SerializeField] float ExplodeVolume;
	[SerializeField] AudioClip ExplodeSFX;

	void Start()
	{
		GetComponent<ExplosiveEntity>().OnExplode.AddListener(SpawnVFX);
	}

	void SpawnVFX()
	{
		Instantiate(VFXPrefab, transform.position, Quaternion.identity);

		SFXManager.PlaySound(ExplodeSFX, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup, ExplodeVolume, Random.Range(0.95f, 1.05f));
	}
}
