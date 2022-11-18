using UnityEngine;

public class DestructionVFX : MonoBehaviour
{
	[SerializeField] GameObject VFXPrefab;

	void Start()
	{
		GetComponent<ExplosiveEntity>().OnExplode.AddListener(SpawnVFX);
	}

	void SpawnVFX()
	{
		Instantiate(VFXPrefab, transform.position, Quaternion.identity);
	}
}
