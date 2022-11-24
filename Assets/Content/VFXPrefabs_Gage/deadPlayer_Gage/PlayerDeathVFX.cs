using UnityEngine;

public class PlayerDeathVFX : MonoBehaviour
{
	[SerializeField] GameObject playerVisualStuff;
	[SerializeField] GameObject playerCollider;
	[SerializeField] GameObject playerModel;
	[SerializeField] GameObject deathVFXPrefab;

	[SerializeField] float DieSFXVolume;
	[SerializeField] AudioClip DieSFX;

	void OnGameEnd()
	{
		playerVisualStuff.SetActive(false);
		playerCollider.SetActive(false);
		Instantiate(deathVFXPrefab, transform.position, playerModel.transform.rotation);
		SFXManager.PlaySound(DieSFX, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup, DieSFXVolume, 1);
	}
}
