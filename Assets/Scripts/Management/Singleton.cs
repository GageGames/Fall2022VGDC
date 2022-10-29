using UnityEngine;

public class Singleton<T> where T : MonoBehaviour
{
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				Init();
			}
			return instance;
		}
		private set
		{
			instance = value;
		}
	}

	static T instance = default(T);

	static void Init()
	{
		instance = new GameObject("SFX Manager").AddComponent<T>();
	}
}
