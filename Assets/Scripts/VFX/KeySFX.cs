using UnityEngine;

public class KeySFX : MonoBehaviour
{
	[SerializeField] float PickupVolume;
	[SerializeField] AudioClip PickupSFX;

	void Start()
	{
		GetComponent<Key>().OnKeyPickup.AddListener(Pickup);
	}

	void Pickup()
	{
		SFXManager.PlaySound(PickupSFX, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup, PickupVolume, 1);
	}
}
