using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	public static MusicPlayer Instance;

	private void Start()
	{
		if (Instance)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		Instance = this;
	}
}
