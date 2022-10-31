using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public bool IsPaused { get; private set; }

	public void Pause()
	{
		Instantiate(GlobalData.GlobalConfigInstance.PauseMenuPrefab);
		IsPaused = true;
	}

	public void Unpause()
	{
		IsPaused = false;
	}

	public void InitiateSceneLoad(string sceneName)
	{
		// TODO: Loading screens, asynch loading for non-web versions
		SceneManager.LoadScene(sceneName);
	}

	void BeginGame()
	{
		BroadcastAll("OnGameBegin", null);
	}

	void EndGame()
	{
		BroadcastAll("OnGameEnd", null);
	}

	void PauseGame()
	{
		BroadcastAll("OnGamePause", null);
	}

	void UnpauseGame()
	{
		BroadcastAll("OnGameUnpause", null);
	}

	public static void BroadcastAll(string methodName, System.Object parameter)
	{
		// Get... every gameobject
		// Yes, every
		// this_is_fine.jpg
		// (no really it's fine just don't abuse this)
		GameObject[] allGameObjects = (GameObject[]) FindObjectsOfType(typeof(GameObject));

		// Ensure that each gameobject exists and has no parent
		// Note that BroadcastMessage gets sent to all child objects in a heirarchy
		List<GameObject> rootGameObjects = allGameObjects.ToList().FindAll(obj => (obj != null && obj.transform.parent != null));

		foreach (GameObject gameobj in rootGameObjects)
		{
			gameobj.BroadcastMessage(methodName, parameter, SendMessageOptions.DontRequireReceiver);
		}
	}
}
