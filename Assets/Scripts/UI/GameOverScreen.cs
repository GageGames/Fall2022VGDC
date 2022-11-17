using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private float soundVolume = 1;
    private void PlayButtonSound()
    {
        SFXManager.PlaySound(buttonSound,soundVolume,1);
    }
    public void Restart()
	{
        PlayButtonSound();
        Singleton<GameManager>.Instance.InitiateSceneLoad("Main");
	}
    
    public void ToMainMenu()
    {
        PlayButtonSound();
        Singleton<GameManager>.Instance.InitiateSceneLoad("MainMenu");
    }
}
