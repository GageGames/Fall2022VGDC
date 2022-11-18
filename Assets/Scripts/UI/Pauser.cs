using UnityEngine;

public class Pauser : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Singleton<GameManager>.Instance.Pause();
		}
	}
}
