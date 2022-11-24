using UnityEngine;

public class PlayerDeathVFX : MonoBehaviour
{
	[SerializeField] GameObject playerVisualStuff;
	[SerializeField] GameObject playerCollider;
	[SerializeField] GameObject playerModel;
	[SerializeField] GameObject deathVFXPrefab;

	void OnGameEnd()
	{
		playerVisualStuff.SetActive(false);
		playerCollider.SetActive(false);
		Instantiate(deathVFXPrefab, transform.position, playerModel.transform.rotation);
	}
}
