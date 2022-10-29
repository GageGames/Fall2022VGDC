using UnityEngine;

public class GameManager : MonoBehaviour
{
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
