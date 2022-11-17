using UnityEngine;

public class ExplosiveVFX : MonoBehaviour
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
