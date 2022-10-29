using UnityEngine;

public static class Singleton<T> where T : MonoBehaviour
{
	private static readonly object padlock = new object();

	public static T Instance
	{
		get
		{
			lock (padlock)
			{
				if (instance == null || instance == default(T))
				{
					Init();
				}
				return instance;
			}
		}
	}

	static T instance = default(T);

	static void Init()
	{
		instance = new GameObject(typeof(T).Name).AddComponent<T>();
	}
}
