using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject quitButton;
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_WEBGL
            quitButton.setActive(false);
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToTutorial()
	{
		Singleton<GameManager>.Instance.InitiateSceneLoad("Main");
	}

	public void QuitGame()
	{
		Singleton<GameManager>.Instance.QuitGame();
	}
}
