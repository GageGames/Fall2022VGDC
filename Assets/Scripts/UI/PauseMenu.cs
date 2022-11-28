using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private AudioClip buttonSound;
	[SerializeField] private float soundVolume = 1;

	private void PlayButtonSound()
	{
		SFXManager.PlaySound(buttonSound, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup, soundVolume, 1);
	}

	public void Unpause()
	{
		PlayButtonSound();
		GetComponent<PauseMenuCallback>().ResumeGame();
	}

	public void ToMainMenu()
	{
		print("clicked please");
		PlayButtonSound();
		Singleton<GameManager>.Instance.Unpause();
		Singleton<GameManager>.Instance.InitiateSceneLoad("MainMenu");
	}

	public void QuitGame()
	{
		PlayButtonSound();
		Singleton<GameManager>.Instance.QuitGame();
	}
}
