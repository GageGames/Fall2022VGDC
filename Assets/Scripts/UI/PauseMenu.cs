using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	[SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private float soundVolume = 1;
    
    private void PlayButtonSound()
    {
        SFXManager.PlaySound(buttonSound,soundVolume,1);
    }

	public void Unpause()
	{
        PlayButtonSound();
        GetComponent<PauseMenuCallback>().ResumeGame();
	}
    
    public void ToMainMenu()
    {
        PlayButtonSound();
        Singleton<GameManager>.Instance.InitiateSceneLoad("MainMenu");
    }
    
    public void QuitGame()
    {
        PlayButtonSound();
        Singleton<GameManager>.Instance.QuitGame();
    }
}
