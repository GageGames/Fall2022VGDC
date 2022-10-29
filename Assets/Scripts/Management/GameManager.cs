using UnityEngine;

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
}
