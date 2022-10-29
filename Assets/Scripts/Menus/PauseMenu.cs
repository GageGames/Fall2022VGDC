using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	[SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private float soundVolume = 1;
    
    void PlayButtonSound()
    {
        // Singleton<SFXManager>.Instance.PlaySound(buttonSound,soundVolume,1);
    }

	void Unpause()
	{
        PlayButtonSound();
        // callback shit
	}
    
    void ToMainMenu()
    {
        PlayButtonSound();
        // GameManager.LoadScene("MainMenu");
    }
    
    void QuitGame()
    {
        PlayButtonSound();
        Application.Quit();
        // GameManager.Quit();
    }
}
