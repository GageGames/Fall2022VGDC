using UnityEngine;

public class PauseMenuCallback : MonoBehaviour
{
	public void ResumeGame()
	{
		Destroy(gameObject);
		Singleton<GameManager>.Instance.Unpause();
	}
}
