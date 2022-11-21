using UnityEngine;

public class SFXTest : MonoBehaviour
{
	public AudioClip audioClip;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			SFXManager.PlaySound(audioClip, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup);
		}
		if (Input.GetMouseButtonDown(1))
		{
			SFXManager.PlayLoopedSound(audioClip, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup, () => Input.GetMouseButtonUp(1), transform.position, transform);
		}
	}
}
