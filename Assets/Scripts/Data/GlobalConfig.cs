using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NewGlobalConfig", menuName = "Configs/Global Config")]
[System.Serializable]
public class GlobalConfig : ScriptableObject
{
	public GameObject PauseMenuPrefab;
	public GameObject GameOverMenuPrefab;
	public GameObject SceneTransitionOverlayPrefab;

	public AudioMixerGroup SFXMixerGroup;

	[Expandable]
	public GameplayTuningValues PrimaryGameplayTuningValues;
}
