using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private AudioClip buttonSound;
	[SerializeField] private float soundVolume = 1;
	private void PlayButtonSound()
	{
		SFXManager.PlaySound(buttonSound, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup, soundVolume, 1);
	}
	public void Restart()
	{
		PlayButtonSound();
		Singleton<GameManager>.Instance.InitiateSceneLoad(SceneManager.GetActiveScene().name);
	}

	public void ToMainMenu()
	{
		PlayButtonSound();
		Singleton<GameManager>.Instance.InitiateSceneLoad("MainMenu");
	}
}
