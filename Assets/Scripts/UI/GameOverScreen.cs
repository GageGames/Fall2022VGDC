using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverScreen : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private AudioClip buttonSound;
	[SerializeField] private float soundVolume = 1;
    [SerializeField] GameObject UIContainer;
    [SerializeField] float fadeInTime;
    [SerializeField] float delayBeforeFadeIn;
	private void PlayButtonSound()
	{
		SFXManager.PlaySound(buttonSound, Singleton<GlobalData>.Instance.GlobalConfigInstance.SFXMixerGroup, soundVolume, 1);
	}
    void Awake()
    {
        UIContainer.GetComponent<CanvasGroup>().alpha = 0.0f;
    }
    void Start() 
    {
        UIContainer.GetComponent<CanvasGroup>().DOFade(1.0f, fadeInTime).SetDelay(delayBeforeFadeIn).SetEase(Ease.InOutSine);
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
