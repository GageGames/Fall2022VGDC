using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private void Awake()
	{
		if (Singleton<GameManager>.Instance != null && Singleton<GameManager>.Instance != this)
		{
			Destroy(gameObject);
		}
	}

	public bool IsPaused { get; private set; }

	public void Pause()
	{
		Instantiate(GlobalData.GlobalConfigInstance.PauseMenuPrefab);
		IsPaused = true;
	}

	public void UnPause()
	{
		IsPaused = false;
	}

	public void InitiateSceneLoad(string sceneName)
	{
		// TODO: Loading screens, asynch loading for non-web versions
		SceneManager.LoadScene(sceneName);
	}
}
