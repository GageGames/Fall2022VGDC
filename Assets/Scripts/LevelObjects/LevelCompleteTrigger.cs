using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
	public string NextSceneName;

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponentInParent<Player>())
		{
			Singleton<GameManager>.Instance.InitiateSceneLoad(NextSceneName);
		}
	}
}
